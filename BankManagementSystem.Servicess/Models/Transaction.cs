using BankManagementSystem.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Servicess.Models
{
    public class Transaction
    {

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
        public TransType TransactionType { get; set; }


        public DateTime TransactionDate { get; set; }


        public long AccountNumber { get; set; }

       
    }
}
