using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Pedidos.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Pedidos.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {

        private readonly PedidosContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public VoucherRepository(PedidosContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
        public IUnitOfWork UnitOfWork => _context;
>>>>>>> Configuring MessageBus na API de Carrinho e Catalago
=======
>>>>>>> Fixed Voucher Validation Problem
=======
>>>>>>> 488619536331e5337e0788f8170cd25d33ff7cf3

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public void Atualizar(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
