using BankManagementSystem.Domain.Enum;
using BankManagementSystem.Model;

namespace BankManagementSystem.models
{
    public class TransactionPersonalDetails
    {
        public int TransactionId { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public TransType TransactionType { get; set; }


        public DateTime TransactionDate { get; set; }

        public long AccountNumber { get; set; }

        //public CustomerDetails? CustomerDetails { get; set; }
    }
}
