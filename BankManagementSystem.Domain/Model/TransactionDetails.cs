using BankManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankManagementSystem.Domain.Enum;

namespace BankManagementSystem.Domain.Model
{
    public class TransactionDetails
    {
        [Key]

        public int TransactionId { get; set; }

        public decimal Amount { get; set; }

        public TransType TransactionType { get; set; }


        public DateTime TransactionDate { get; set; }

        public decimal Balance { get; set; }


        [ForeignKey("CustomerDetails")]

        public long AccountNumber { get; set; }
    
        public CustomerDetails? CustomerDetails { get; set; }
    }
}
