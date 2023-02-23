using BankManagementSystem.Domain.Context;
using BankManagementSystem.Domain.Enum;
using BankManagementSystem.Domain.InterfaceRepository;
using BankManagementSystem.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Domain.Repository
{
    public class MoneyTransactionRepository : IMoneyTransactionRepository
    {
        private readonly DataContext _dbContext;
        

        public MoneyTransactionRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async Task<List<MoneyTransactionDetails>> GetMoneyTransactionById(long AccountNumber)
        {
            var user = await _dbContext.Customers.FindAsync(AccountNumber);
            if (user == null)
                return null;

            var Transaction = await _dbContext.FundTransactions.Where(b => b.AccountNumber == AccountNumber).ToListAsync();
            return Transaction;
        }
        public async Task<MoneyTransactionDetails> PostTransactionAsync(MoneyTransactionDetails requests)
        {

           // if (_type == TransactionTypes.Deposit)
            if (requests.TransactionType == TransactionTypes.Deposit)
             {
                var AccountNumber = await _dbContext.Customers.FindAsync(requests.AccountNumber);
                var DestinationAccount = await _dbContext.Customers.FindAsync(requests.DestinationAccountNumber);

                if (AccountNumber == null) { return null; }
                else
                {

                    var Details = new MoneyTransactionDetails
                    {
                        AccountNumber = requests.AccountNumber,
                        DestinationAccountNumber = requests.DestinationAccountNumber,
                        TransactionDate = DateTime.Now,
                        Amount = requests.Amount,
                        TransactionType = requests.TransactionType,
                        CustomerDetails = AccountNumber
                    };

                    AccountNumber.CurrentBalance -= requests.Amount;
                    DestinationAccount.CurrentBalance += requests.Amount;
                    await _dbContext.FundTransactions.AddAsync(Details);
                    await _dbContext.SaveChangesAsync();
                    return Details;
                }
            }
            return (requests);
        }





    }
}
