//using BankManagementSystem.Domain.Entities.Dto;
using BankManagementSystem.Domain.Enum;
using BankManagementSystem.Domain.Model;

namespace BankManagementSystem.Domain.InterfaceRepository
{
    public interface ITransactionRepository
    {
        Task<List<TransactionDetails>> GetTransactionsByIdAsync();

        Task<List<TransactionDetails>> GetTransactionsDetailsAsync(long AccountNumber);
        Task<List<TransactionDetails>> GetLastTwoTransactionsByIdAsync(long AccountNumber);

        Task<TransactionDetails> PostTransactionAsync(TransactionDetails requests);


    }
}



































//Task<TransactionDetails> PostTransactionAsync(SelectTransactionDetails requests ,TransType type);