using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NSE.Pagamento.NerdsPag
{
    public class Card
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCvv { get; set; }

       
    }
}
