
using NSE.Core.DomainObjects;
using System;

namespace NSE.Pedido.Domain
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Codigo { get; set; }
        public decimal? Percentual { get; set; }
        public decimal? ValorDesconto { get; set; }
        public int Quantidade { get; set; }
        public TipoDescontoVoucher TipoDesconto { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUtilizacao { get; set; }
        public DateTime DataValidade { get; set; }
        public bool Ativo { get; set; }
        public bool Utilizado { get; set; }

    }
}
