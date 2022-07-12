using System;
using System.Collections.Generic;
using System.Text;

namespace NSE.Pagamento.NerdsPag
{
    public enum TransactionStatus
    {
        Authorized =1,
        Paid,
        Refused,
        ChargeBack,
        Cancelled
    }
}
