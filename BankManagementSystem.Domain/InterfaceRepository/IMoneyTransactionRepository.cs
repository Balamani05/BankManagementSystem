using BankManagementSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Domain.InterfaceRepository
{
    public interface IMoneyTransactionRepository
    {
        Task<List<MoneyTransactionDetails>> GetMoneyTransactionById(long AccountNumber);
        Task<MoneyTransactionDetails> PostTransactionAsync(MoneyTransactionDetails requests);
    }
}
