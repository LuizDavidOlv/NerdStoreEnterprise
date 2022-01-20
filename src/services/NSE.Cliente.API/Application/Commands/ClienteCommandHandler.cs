
using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using System.Threading;
using System.Threading.Tasks;


namespace NSE.Cliente.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler,IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
            {
                return message.ValidationResult;
            }

            var cliente = new NSE.Cliente.API.Models.Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            return message.ValidationResult;
        }

    }
}
