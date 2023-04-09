using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Models;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebApi.Core.Controllers;
using NSE.WebApi.Core.Usuario;
using Polly;

namespace NSE.Cliente.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAspNetUser _user;

        public ClientesController(IMediatorHandler mediatorHandler, IAspNetUser user, IClienteRepository clienteRepository)
        {
            _mediatorHandler = mediatorHandler;
            _user = user;
            _clienteRepository = clienteRepository;
        }

        [HttpGet("cliente/endereco")]
        public async Task<IActionResult> ObterEndereco()
        {
            var endereco = await _clienteRepository.ObterEnderecoPorId(_user.ObterUserId());

            return endereco == null ? NotFound() : CustomResponse(endereco);
        }

        [HttpPost("cliente/endereco")]
        public async Task<IActionResult> AdicionarEndereco(AdicionarEnderecoCommand endereco)
        {
            endereco.ClienteId = _user.ObterUserId();
            return CustomResponse(await _mediatorHandler.EnviarComando(endereco));
        }

        [HttpPost("cliente/criar")]
        public async Task<IActionResult> CriarCliente(RegistrarClienteCommand cliente)
        {
            cliente.Id = _user.ObterUserId();
            return CustomResponse(await _mediatorHandler.EnviarComando(cliente));
        }



    }
}
