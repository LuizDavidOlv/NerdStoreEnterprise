using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE.WebApi.Core.Controllers
{
    [ApiController]
    public class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();


        protected ActionResult CustomResponse(object result = null)
        {
            if (!OperacaoValida()) return Ok(result);

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Mensagens",Erros.ToArray() }
            }));
        }


        protected ActionResult CustomReponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in Erros)
            {
                AdicionarErrosProcessamento(erro);
            }

            return CustomResponse();
        }

        protected ActionResult CustomReponse(ValidationResult validationResult)
        {

            foreach (var erro in validationResult.Errors)
            {
                AdicionarErrosProcessamento(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected bool OperacaoValida()
        {
            return Erros.Any();
        }

        protected void AdicionarErrosProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}
