
using BankManagementSystem.Domain.Model;
using BankManagementSystem.Servicess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Servicess.InterfaceService
{
    public interface ITransactionService
    {
        Task<List<TransactionDetails>> GetTransactionsByIdAsync();

        Task<List<TransactionModel>> GetTransactionsDetailsAsync(long AccountNumber);

        Task<List<TransactionModel>> GetLastTwoTransactionsByIdAsync(long AccountNumber);

        Task<Transaction> PostTransactionAsync(Transaction requests);
    }
}
