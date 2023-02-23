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
    public class MoneyTransactionService : IMoneyTransactionService
    {
        private readonly IMoneyTransactionRepository _MoneyTransRepo;
        private readonly IMapper _mapper;

        public MoneyTransactionService(IMoneyTransactionRepository MoneyTransRepo, IMapper mapper)
        {
            _MoneyTransRepo = MoneyTransRepo;
            _mapper = mapper;
        }

        public async Task<List<GetMoneyTransById>> GetMoneyTransactionsById(long AccountNumber)
        {
            var result = await _MoneyTransRepo.GetMoneyTransactionById(AccountNumber);
            return _mapper.Map<List<GetMoneyTransById>>(result);
        }
        public async Task<MoneyTransaction> PostTransactionAsync(MoneyTransaction requests)
        {

            return _mapper.Map<MoneyTransaction>(await _MoneyTransRepo.PostTransactionAsync(_mapper.Map<MoneyTransactionDetails>(requests)));
        }
    }
}
