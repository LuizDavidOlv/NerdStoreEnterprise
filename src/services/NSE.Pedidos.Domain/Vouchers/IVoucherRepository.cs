﻿using NSE.Core.Data;
using System.Threading.Tasks;

namespace NSE.Pedidos.Domain.Vouchers
{
    public  interface IVoucherRepository :IRepository<Voucher>
    {
        Task<Voucher> ObterVoucherPorCodigo(string codigo);
        void Atualizar(Voucher voucher);
    }
}
