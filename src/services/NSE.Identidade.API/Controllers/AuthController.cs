using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Core.Http;
using NSE.Core.Messages.Integration;
using NSE.Identidade.API.Services;
using NSE.MessageBus;
using NSE.WebApi.Core.Controllers;
using System;
using System.Threading.Tasks;
using static NSE.Identidade.API.Models.UserViewModels;

namespace NSE.Identidade.API.Controllers
{

    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly AuthenticationService _authenticationService;
        //private readonly IMessageBus _bus;
        private readonly IKafkaBus _bus;
        private readonly IRestClient _restClient;


        public AuthController(
            AuthenticationService authenticationService,
            IKafkaBus bus,
            IRestClient restClient)
        {
            _authenticationService = authenticationService;
            _bus = bus;
            _restClient = restClient;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = usuarioRegistro.Email,
                Email = usuarioRegistro.Email,
                EmailConfirmed = true
            };

            var result = await _authenticationService.UserManager.CreateAsync(user, usuarioRegistro.Senha);

            if (result.Succeeded)
            {
                var jwt = await _authenticationService.GerarJwt(usuarioRegistro.Email);
                var clienteResult = await RegistrarCliente(usuarioRegistro,jwt);

                if (!clienteResult.ValidationResult.IsValid)
                {
                    await _authenticationService.UserManager.DeleteAsync(user);
                    return CustomResponse(clienteResult.ValidationResult);
                }

                return CustomResponse(jwt);
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }
            return CustomResponse();
        }

        [HttpPost("autenticar")]
        public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _authenticationService.SignInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Senha, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await _authenticationService.GerarJwt(usuarioLogin.Email));
            }

            if(result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AdicionarErroProcessamento("Usuário ou senha inváldos");
            return CustomResponse();
        }


     

  
        private async Task< ResponseMessage> RegistrarCliente(UsuarioRegistro usuarioRegistro, UsuarioRespostaLogin jwt)
        {
            var usuario = await _authenticationService.UserManager.FindByEmailAsync(usuarioRegistro.Email);

            var usuarioRegistrado = new UsuarioRegistradoIntegrationEvent(
                Guid.Parse(usuario.Id), usuarioRegistro.Nome, usuarioRegistro.Email, usuarioRegistro.Cpf);

            try
            {
                var response = await _restClient
                    .PostAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(usuarioRegistrado, jwt.AccessToken);

                return response;
               // return await _bus.RequestAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(usuarioRegistrado);
            }
            catch
            {
                await _authenticationService.UserManager.DeleteAsync(usuario);
                throw;
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AdicionarErroProcessamento("Refresh token inválido");
                return CustomResponse();
            }

            var token = await _authenticationService.ObterRefreshToken(Guid.Parse(refreshToken));
            
            if(token == null)
            {
                AdicionarErroProcessamento("Refresh token expirado");
                return CustomResponse();
            }

            return CustomResponse(await _authenticationService.GerarJwt(token.Username));
        }
    }
}
