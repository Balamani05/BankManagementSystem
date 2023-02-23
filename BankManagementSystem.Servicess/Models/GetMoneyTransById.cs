using BankManagementSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Servicess.Models
{
    public class GetMoneyTransById
    {
        public int TransactionId { get; set; }

        public long DestinationAccountNumber { get; set; }

        public decimal Amount { get; set; }


        public TransactionTypes TransactionType { get; set; }


        public DateTime TransactionDate { get; set; }

        public long AccountNumber { get; set; }
    }
}
