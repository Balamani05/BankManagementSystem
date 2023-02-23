using BankManagementSystem.Domain.Enum;

namespace BankManagementSystem.models
{
    public class PostFundTransaction
    {
        public long DestinationAccountNumber { get; set; }

        public decimal Amount { get; set; }


        public TransactionTypes TransactionType { get; set; }


        public DateTime TransactionDate { get; set; }

        public decimal Balance { get; set; }

        public long AccountNumber { get; set; }
    }
}
