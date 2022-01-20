using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;
using MediatR;

namespace NSE.Core.Messages
{
    public abstract class Command: Message,IRequest<ValidationResult>
    {
        public DateTime TimeStamp { get; set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }



    }
}
