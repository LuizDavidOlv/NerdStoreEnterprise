﻿using NSE.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace NSE.Pagamento.API.Models
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid PedidoId { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public decimal Valor { get; set; }
        public CartaoCredito CartaoCredito { get; set; }


        //EF Relation
        public ICollection<Transacao> Transacoes { get; set; }


        public Pagamento()
        {
            Transacoes = new List<Transacao>();
        }

        public void AdicionarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }
    }
}
