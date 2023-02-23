using Azure;
using Azure.Core;
//using BankManagementSystem.Domain.Entities.Dto;
using BankManagementSystem.Domain.Enum;
using BankManagementSystem.Domain.InterfaceRepository;
using BankManagementSystem.Domain.Model;

using BankManagementSystem.Domain.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Domain.Repository
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly DataContext _dataContext;
        

        public TransactionRepository(DataContext datacontext)
        {
            _dataContext = datacontext;
           
        }

        public async Task<List<TransactionDetails>> GetTransactionsByIdAsync()
        {


            var user = await _dataContext.Customers.ToListAsync();
            if (user == null)
                return null;

            //var Transaction = await _dataContext.Transactions.Where(b => b.AccountNumber == AccountNumber).ToListAsync();
            var Transaction = await _dataContext.Transactions.ToListAsync();
            return Transaction;
        }

        public async Task<List<TransactionDetails>> GetTransactionsDetailsAsync(long AccountNumber)
        {


            var user = await _dataContext.Customers.FindAsync(AccountNumber);
            if (user == null)
                return null;

            var Transaction = await _dataContext.Transactions.Where(b => b.AccountNumber == AccountNumber).ToListAsync();
            return Transaction;
        }

        public async Task<List<TransactionDetails>> GetLastTwoTransactionsByIdAsync(long AccountNumber)
        {


            var user = await _dataContext.Customers.FindAsync(AccountNumber);
            if (user == null)
                return null;

            var Transaction = await _dataContext.Transactions.Where(b => b.AccountNumber == AccountNumber).OrderByDescending(c => c.TransactionId).Take(2).ToListAsync();
            return Transaction;
        }


        public async Task<TransactionDetails> PostTransactionAsync(TransactionDetails requests)
        {

            //var Account = await _dataContext.Customers.FindAsync(requests.AccountNumber);

         //   if (type == TransType.Deposit)
                if (requests.TransactionType == TransType.Deposit)
                {
                var acc = await _dataContext.Customers.FindAsync(requests.AccountNumber);
                if (acc == null) { return null; }
                else
                {
                    var details = new TransactionDetails
                    {
                        Amount = requests.Amount,
                        TransactionDate = DateTime.Now,
                        TransactionType = requests.TransactionType,
                        Balance = acc.CurrentBalance + requests.Amount,
                        AccountNumber = requests.AccountNumber,
                        CustomerDetails = acc
                    };
                    acc.CurrentBalance += requests.Amount;
                    await _dataContext.Transactions.AddAsync(details);
                    await _dataContext.SaveChangesAsync();

                    return details;
                }
            }
            else
            {
                var acc = await _dataContext.Customers.FindAsync(requests.AccountNumber);
                if (acc == null) { return null; }
                else
                {
                    var details = new TransactionDetails
                    {
                        Amount = requests.Amount,
                        TransactionDate = DateTime.Now,
                        TransactionType = requests.TransactionType,
                        Balance = acc.CurrentBalance - requests.Amount,
                        AccountNumber = requests.AccountNumber,
                        CustomerDetails = acc
                    };
                    acc.CurrentBalance -= requests.Amount;
                    await _dataContext.Transactions.AddAsync(details);
                    await _dataContext.SaveChangesAsync();

                    return details;
                }
               

            }

        }
    }
}












//if (Account == null)
//{
//    return null;
//}
//else
//{

//    var newdetail = new TransactionDetails()
//    {
//        Amount = requests.Amount,
//        TransactionDate = DateTime.Now,
//        TransactionType = requests.TransactionType,
//        CustomerDetails = Account

//    };
//    if (requests.TransactionType == TransType.Deposit)
//        Account.CurrentBalance += requests.Amount;

//    else
//    {
//        // requests.TransactionType == TransType.Withdraw
//        Account.CurrentBalance -= requests.Amount;
//    }

//    newdetail.TransactionType = requests.TransactionType;
//    await _dataContext.Transactions.AddAsync(newdetail);
//    await _dataContext.SaveChangesAsync();

//    return newdetail;
//}


































//   if (requests.TransactionType == "Withdraw" || requests.TransactionType == "withdraw")
//    {
//        customer.CurrentBalance -= requests.Amount;
//    }
//    else
//    {
//        customer.CurrentBalance += requests.Amount;
//    }

//    await _dataContext.Transactions.AddAsync(newdetail);
//    await _dataContext.SaveChangesAsync();
//    return newdetail;

//     }

//public async Task<TransactionDetails> PostTransactionAsync(SelectTransactionDetails requests, TransType type)
//{

//    TransactionDetails transaction = new TransactionDetails();


//    if (type == TransType.Deposit)
//    {
//        var Account = await _dataContext.Customers.FindAsync(requests.AccountNumber);


//        if (Account == null)
//        {
//            return null;
//        }
//        else
//        {

//            var newdetail = new TransactionDetails()
//            {
//                Amount = requests.Amount,
//                TransactionDate = DateTime.Now,
//                CustomerDetails = Account

//            };

//            Account.CurrentBalance += requests.Amount;
//            newdetail.TransactionType = type;
//            await _dataContext.Transactions.AddAsync(newdetail);
//            await _dataContext.SaveChangesAsync();

//            return newdetail;
//        }
//    }
//    else
//    {
//        var Account = await _dataContext.Customers.FindAsync(requests.AccountNumber);

//        if (Account != null)
//        {

//            var newdetail = new TransactionDetails()
//            {
//                Amount = requests.Amount,
//                TransactionDate = DateTime.Now,
//                CustomerDetails = Account,

//            };

//            Account.CurrentBalance -= requests.Amount;
//            transaction.TransactionType = type;

//            newdetail.TransactionType = type;

//            await _dataContext.Transactions.AddAsync(newdetail);


//            await _dataContext.SaveChangesAsync();

//            return newdetail;

//        }

//    }
//    return transaction;



//}








////var customer = await _dataContext.Customers.FindAsync(requests.CustomerId);
//var customer = await _dataContext.Customers.FindAsync(requests.CustomerId);
//    if (customer == null)
//        return null;



//    var newdetail = new TransactionDetails
//    {
//        Amount = requests.Amount,
//        TransactionDate = requests.TransactionDate,
//         TransactionType = requests.TransactionType,
//        CustomerDetails = customer
//    };

//    if (requests.TransactionType == "Withdraw" || requests.TransactionType == "withdraw")
//    {
//        customer.CurrentBalance -= requests.Amount;
//    }
//    else
//    {
//        customer.CurrentBalance += requests.Amount;
//    }

//    await _dataContext.Transactions.AddAsync(newdetail);
//    await _dataContext.SaveChangesAsync();
//    return newdetail;




//TransactionDetails transaction = new TransactionDetails();
//try
//{

//    if (type == TransType.Deposit)
//    {
//        var Account = _dataContext.Customers.Find(requests.CustomerId);

//        if (Account != null)
//        {
//            var newdetail = new TransactionDetails()
//            {
//                Amount = requests.Amount,
//                TransactionDate = DateTime.Now,
//                Customers = Account,

//            };

//            Account.CurrentBalance += requests.Amount;
//            newdetail.TransactionType = type;
//            _dataContext.Transactions.Add(newdetail);
//            _dataContext.SaveChanges();
//            response.ResponseMessage = "Transaction Successful!";
//        }
//        else
//        {

//            response.ResponseMessage = "Transaction Failed!";

//        }
//    }
//    else
//    {
//        var Account = _dataContext.Customers.Find(requests.CustomerId);

//        if (Account != null)
//        {

//            var newdetail = new TransactionDetails()
//            {
//                Amount = requests.Amount,
//                TransactionDate = DateTime.Now,
//                Customers = Account,

//            };

//            Account.CurrentBalance -= requests.Amount;
//            transaction.TransactionType = type;

//            newdetail.TransactionType = type;

//            _dataContext.Transactions.Add(newdetail);


//            _dataContext.SaveChanges();
//            response.ResponseMessage = "Transaction Successful!";
//        }
//        else
//        {

//            response.ResponseMessage = "Transaction Failed!";
//        }
//    }

//}
//catch (Exception error)
//{
//    throw error;
//}
//return response;
//            }




//public async Task<TransactionDetails> PostTransactionAsync(SelectTransactionDetails requests)
//{
//    //var customer = await _dataContext.Customers.FindAsync(requests.CustomerId);
//    var customer = await _dataContext.Customers.FindAsync(requests.CustomerId);
//    if (customer == null)
//        return null;



//    var newdetail = new TransactionDetails
//    {
//        Amount = requests.Amount,
//        TransactionDate = requests.TransactionDate,
//        //   TransactionType = requests.TransactionType,
//        CustomerDetails = customer
//    };

//    if (requests.TransactionType == "Withdraw" || requests.TransactionType == "withdraw")
//    {
//        customer.CurrentBalance -= requests.Amount;
//    }
//    else
//    {
//        customer.CurrentBalance += requests.Amount;
//    }

//    await _dataContext.Transactions.AddAsync(newdetail);
//    await _dataContext.SaveChangesAsync();
//    return newdetail;
//}