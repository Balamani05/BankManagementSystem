using AutoMapper;
using BankManagementSystem.Domain.InterfaceRepository;
using BankManagementSystem.Domain.Model;
using BankManagementSystem.Servicess.InterfaceService;
using BankManagementSystem.Servicess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Servicess.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _TransRepo;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transRepo, IMapper mapper)
        {
            _TransRepo = transRepo;
            _mapper = mapper;
        }

        public async Task<List<TransactionDetails>> GetTransactionsByIdAsync()
        {

            return await _TransRepo.GetTransactionsByIdAsync();
        }

        public async Task<List<TransactionModel>> GetTransactionsDetailsAsync(long AccountNumber)
        {
            var res = await _TransRepo.GetTransactionsDetailsAsync(AccountNumber);
            return _mapper.Map<List<TransactionModel>>(res);
        }

        public async Task<List<TransactionModel>> GetLastTwoTransactionsByIdAsync(long AccountNumber)
        {
            var res = await _TransRepo.GetLastTwoTransactionsByIdAsync(AccountNumber);
            return _mapper.Map<List<TransactionModel>>(res);


            // return await _TransRepo.GetLastTwoTransactionsByIdAsync(AccountNumber);

        }

        public async Task<Transaction> PostTransactionAsync(Transaction requests)
        {

            return _mapper.Map<Transaction>(await _TransRepo.PostTransactionAsync(_mapper.Map<TransactionDetails>(requests)));
        }



    }
}
