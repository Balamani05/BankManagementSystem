
using BankManagementSystem.Domain.Model;
using BankManagementSystem.Dto;
using BankManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Dto.Interface
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDetails>> GetCustomerAsync();

        Task<CustomerDetails> GetCustomerDetailsByIdAsync(long AccountNumber);
        Task<CustomerDetails> GetCustomerBankByIdAsync(long AccountNumber);
        Task<CustomerDetails> GetCurrentBalanceByIdAsync(long AccountNumber);
        Task<CustomerDetails> CreateCustomerAsync(CustomerDetails customer);
        Task<CustomerDetails> UpdateCustomerMobileNumberAsync(CustomerDetails customer,long AccountNumber);
        Task<CustomerDetails> DeleteCustomerAsync(long AccountNumber);

      
    }
}












//Task<List<CustomerDetails>> GetCustomerAsync();
//Task<List<CustomerDetails>> GetAllCustomerNameAsync();
//Task<CustomerDetails> GetCustomerBankByIdAsync(long AccountNumber);










//Task<bool> CreateCustomerDetailsAsync(CustomerDetails customer);

//Task<CustomerDetails> CreateCustomerAsync(CustomerDetails customer);
//Task<CustomerDetails> UpdateCustomerAsync(long AccountNumber, CustomerDetails customer);

//Task<bool> UpdateCustomerPersonalDetailsAsync(long AccountNumber, CustomerDetails customer);


//Task<CustomerDetails> CreateAsync(CustomerDetails customer);
//Task<CustomerDetails> CreateCustomerDetailsAsync(CreateCustomer customer);


//Task<CustomerDetails> UpdateCustomerBankDetailsByIdAsync(int id, CustomerDetails customer);
//Task<List<CustomerDetails>> GetCustomerTableAsync();
//Task<CustomerDetails> GetCustomerDetailsAsync(int id);

//Task<CustomerDetails> CreateCustomerDetailsAsync(CustomerDetails customerDetails);
//Task<CustomerDetails> UpdateCustomerDetailsAsync(int id, CustomerDetails customerDetails);

//Task<CustomerDetails> RemoveCustomerDetailsAsync(int id);


//CustomerDetails GetCustomerById(int id);
//List<CustomerDetails> GetCustomer();
//bool CreateCustomer(CustomerDetails customer);
//bool UpdateCustomer(int id, CustomerDetails employee);
//bool DeleteCustomer(int id);