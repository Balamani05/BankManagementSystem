using AutoMapper;
using BankManagementSystem.Dto.Interface;
using BankManagementSystem.Model;
using BankManagementSystem.Servicess.InterfaceService;
using BankManagementSystem.Servicess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Servicess.Services
{
    public class CustomerServices : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;

        public CustomerServices(ICustomerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<GetAllPersonalCustomerDetails>> GetCustomerAsync()
        {
            var res = await _repo.GetCustomerAsync();
            return _mapper.Map<List<GetAllPersonalCustomerDetails>>(res);
        }



        public async Task<GetAllCustomerDetailsId> GetCustomerDetailsByIdAsync(long AccountNumber)
        {
            var res = await _repo.GetCustomerDetailsByIdAsync(AccountNumber);
            var result = _mapper.Map<GetAllCustomerDetailsId>(res);

            return result;
        }


        public async Task<GetAllCustomerDetailsId> GetCustomerBankByIdAsync(long AccountNumber)
        {
            var res = await _repo.GetCustomerBankByIdAsync(AccountNumber);
            return _mapper.Map<GetAllCustomerDetailsId>(res);

            
        }

        public async Task<GetAllCustomerDetailsId> GetCurrentBalanceByIdAsync(long AccountNumber)
        {
            var res = await _repo.GetCurrentBalanceByIdAsync(AccountNumber);
            return _mapper.Map<GetAllCustomerDetailsId>(res);
        }


        public async Task<CustomerModel> CreateCustomerAsync(CustomerModel customerDetails)
        {

            var result = await _repo.CreateCustomerAsync(_mapper.Map<CustomerDetails>(customerDetails));
            return _mapper.Map<CustomerModel>(result);
        }

        public async Task<UpdateMobileNumber> UpdateCustomerMobileNumberAsync(UpdateMobileNumber customerDetail,long AccountNumber)
        {

            //var res = await _repo.UpdateCustomerMobileNumberAsync(_mapper.Map<CustomerDetails>(customerDetail));
            //return _mapper.Map<UpdateMobileNumber>(res);

            return _mapper.Map<UpdateMobileNumber>(await _repo.UpdateCustomerMobileNumberAsync(_mapper.Map<CustomerDetails>(customerDetail),AccountNumber));
        }

        public async Task<CustomerDetails> DeleteCustomerAsync(long AccountNumber)
        {

            return await _repo.DeleteCustomerAsync(AccountNumber);
        }
    }
}





















//public async Task<List<GetAllPersonalCustomerDetails>> GetCustomerAsync()
//{
//    var res = await _repo.GetCustomerAsync();
//    return _mapper.Map<List<GetAllPersonalCustomerDetails>>(res);
//}

//public async Task<List<GetAllPersonalCustomerDetails>> GetAllCustomerNameAsync()
//{
//    var res = await _repo.GetAllCustomerNameAsync();
//    return _mapper.Map<List<GetAllPersonalCustomerDetails>>(res);

//    //  return await _repo.GetAllCustomerNameAsync();
//}






//public async Task<bool> UpdateCustomerPersonalDetailsAsync(CustomerModel customerDetail)
//{

//    return await _repo.UpdateCustomerPersonalDetailsAsync(_mapper.Map<CustomerDetails>(customerDetail));

//}

//public async Task<bool> UpdateCustomerMobileNumberAsync(CustomerModel customerDetail)
//{

//    return await _repo.UpdateCustomerMobileNumberAsync(_mapper.Map<CustomerDetails>(customerDetail));

//}

//public async Task<CustomerDetails> DeleteCustomerAsync(long AccountNumber)
//{

//    return await _repo.DeleteCustomerAsync(AccountNumber);
//}












