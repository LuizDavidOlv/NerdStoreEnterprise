﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSE.Core.Messages
{
    public class Event : Message, INotification
    {
        public DateTime TimeStamp { get; set; }

        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
