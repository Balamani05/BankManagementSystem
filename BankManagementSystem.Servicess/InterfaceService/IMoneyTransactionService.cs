using BankManagementSystem.Domain.Model;
using BankManagementSystem.Servicess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankManagementSystem.Servicess.InterfaceService
{
    public interface IMoneyTransactionService
    {
        Task<List<GetMoneyTransById>> GetMoneyTransactionsById(long AccountNumber);
        Task<MoneyTransaction> PostTransactionAsync(MoneyTransaction requests);
    }
}
