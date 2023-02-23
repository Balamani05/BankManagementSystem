using BankManagementSystem.Model;
using BankManagementSystem.Servicess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Servicess.InterfaceService
{
    public interface ICustomerService
    {
        Task<List<GetAllPersonalCustomerDetails>> GetCustomerAsync();
        Task<GetAllCustomerDetailsId> GetCustomerDetailsByIdAsync(long AccountNumber);
        Task<GetAllCustomerDetailsId> GetCustomerBankByIdAsync(long AccountNumber);
        Task<GetAllCustomerDetailsId> GetCurrentBalanceByIdAsync(long AccountNumber);
        Task<CustomerModel> CreateCustomerAsync(CustomerModel customer);
        Task<UpdateMobileNumber> UpdateCustomerMobileNumberAsync( UpdateMobileNumber customer,long AccountNumber);
        Task<CustomerDetails> DeleteCustomerAsync(long AccountNumber);
    }
}




























//Task<List<GetAllPersonalCustomerDetails>> GetCustomerAsync();
//Task<List<GetAllPersonalCustomerDetails>> GetAllCustomerNameAsync();


//Task<bool> UpdateCustomerPersonalDetailsAsync(CustomerModel customer);

//Task<CustomerDetails> DeleteCustomerAsync(long AccountNumber);