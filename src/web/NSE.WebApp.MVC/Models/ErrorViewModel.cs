using System;
using System.Collections.Generic;

namespace NSE.WebApp.MVC.Models
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErroMessages Errors { get; set; }

        public ResponseResult()
        {
            Errors = new ResponseErroMessages();
        }
    }

    public class ResponseErroMessages
    {
        public List<string> Mensagens { get; set; }
        public ResponseErroMessages()
        {
            Mensagens = new List<string>();
        }
    }

}
