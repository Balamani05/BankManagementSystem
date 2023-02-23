using BankManagementSystem.Domain.Enum;

namespace BankManagementSystem.models
{
    public class GetFundTransactionById
    {
        public int TransactionId { get; set; }

        public long DestinationAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public TransactionTypes TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }

        public long AccountNumber { get; set; }
    }
}
