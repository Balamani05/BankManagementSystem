using BankManagementSystem.Domain.Enum;
using BankManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Domain.Model
{
    public class MoneyTransactionDetails
    {
        [Key]

        public int TransactionId { get; set; }


        public long DestinationAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public TransactionTypes TransactionType { get; set; }


        public DateTime TransactionDate { get; set; }




        [ForeignKey("CustomerDetails")]

        public long AccountNumber { get; set; }

        public virtual CustomerDetails? CustomerDetails { get; set; }
    }
}
