using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace NSE.Core.Messages
{
    public abstract class Command: Message
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
