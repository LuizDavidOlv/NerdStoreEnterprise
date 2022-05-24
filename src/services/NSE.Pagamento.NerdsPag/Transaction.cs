using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSE.Pagamento.NerdsPag
{
    public class Transaction
    {
        protected Transaction() { }
        protected string Endpoint { get; set; }
        public int SubscriptionId { get; set; }
        public TransactionStatus Status { get; set; }
        public int AuthorizationAmmount { get; set; }
        public int PaidAmount { get; set; }
        public int RefundedAmount { get; set; }
        public string CardHash { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string StatusReason { get; set; }
        public string AcquirerResponseCode { get; set; }
        public string AcquirerName { get; set; }
        public string AuthorizationCode { get; set; }
        public string SoftDescriptor { get; set; }
        public string RefuseReason { get; set; }
        public string Tid { get; set; }
        public string Nsu { get; set; }
        public decimal Amount { get; set; }
        public int? Installments { get; set; }
        public decimal Cost { get; set; }
        public string CardHolderName { get; set; }
        public string CardCvv { get; set; }
        public string CardLastDigits { get; set; }
        public string CardFirstDigits { get; set; }
        public string CardBrand { get; set; }
        public string CardEmvResponse { get; set; }
        public string PostbackUrl { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public float? AntifraudScore { get; set; }
        public string BilletUrl { get; set; }
        public string BilletIntruction { get; set; }
        public DateTime? BilletExpirationDate { get; set; }
        public string  BillectBarcode { get; set; }
        public string Referer { get; set; }
        public string IP { get; set; }
        public bool? ShouldCapture { get; set; }
        public bool? Async { get; set; }
        public string LocalTime { get; set; }
        public DateTime TransactionoDate { get; set; }


        public Task<Transaction> AuthorizeCardTransaction()
        {

            return null;
        }

        public Task<Transaction> CaputureCardTransaction()
        {
            return null;
        }

        public Task<Transaction> CancelAuthorization()
        {
            return null;
        }

        private string GetGenericCode()
        {
            return null;
        }




    }
}
