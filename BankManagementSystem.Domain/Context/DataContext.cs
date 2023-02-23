using BankManagementSystem.Domain.Model;
using BankManagementSystem.Dto;
using BankManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Domain.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<CustomerDetails> Customers { get; set; }
        
        public DbSet<TransactionDetails> Transactions { get; set; }

        public DbSet<MoneyTransactionDetails> FundTransactions { get; set; }
    }
}
