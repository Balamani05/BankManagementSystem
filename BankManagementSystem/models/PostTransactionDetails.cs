using BankManagementSystem.Domain.Enum;

namespace BankManagementSystem.models
{
    public class PostTransactionDetails
    {


        public decimal Amount { get; set; }

          public TransType TransactionType { get; set; }


        public DateTime TransactionDate { get; set; }


        public long AccountNumber { get; set; }
    }
}
