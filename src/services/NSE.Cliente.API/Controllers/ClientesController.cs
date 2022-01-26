

using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSE.Cliente.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebApi.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Controllers
{
    [ApiController]
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            var resultado = await _mediatorHandler.EnviarComando(new RegistrarClienteCommand(Guid.NewGuid(), "Eduardo", "edu@edu.com", "79111508000"));

            return CustomResponse(resultado);
        }

        

    }
}
