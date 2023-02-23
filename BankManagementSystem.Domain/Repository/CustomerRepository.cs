using BankManagementSystem.Domain.Context;
using BankManagementSystem.Dto.Interface;
using BankManagementSystem.Dto;
using BankManagementSystem.Domain.Repository;
using BankManagementSystem.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
//using BankManagementSystem.Domain.Dto;
using BankManagementSystem.Domain.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BankManagementSystem.Domain.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        private readonly IConfiguration _configuration;


        public CustomerRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<CustomerDetails>> GetCustomerAsync()
        {

            return await _context.Customers.ToListAsync();

        }


        public async Task<CustomerDetails> GetCustomerDetailsByIdAsync(long AccountNumber)
        {

            var customer = await _context.Customers.Where(us => us.AccountNumber == AccountNumber).FirstOrDefaultAsync();

            return customer;

        }
        public async Task<CustomerDetails> GetCustomerBankByIdAsync(long AccountNumber)
        {

            //var customer = await _context.Customers.Where(us => us.CustomerId == id).ToListAsync();
            //return customer;

            var customer = await _context.Customers.Where(us => us.AccountNumber == AccountNumber).FirstOrDefaultAsync();

            if (customer == null)
                throw new Exception($"AccountNumber  {AccountNumber} doesn't exist");

            return customer;

        }
        public async Task<CustomerDetails> GetCurrentBalanceByIdAsync(long AccountNumber)
        {

            var customer = await _context.Customers.Where(us => us.AccountNumber == AccountNumber).FirstOrDefaultAsync();

            return customer;

        }


        public async Task<CustomerDetails> CreateCustomerAsync(CustomerDetails customerDetails)
        {
            await _context.Customers.AddAsync(customerDetails);
            await _context.SaveChangesAsync();

            return customerDetails;
        }
   


      
        public async Task<CustomerDetails> UpdateCustomerMobileNumberAsync(CustomerDetails customerDetails,long AccountNumber)
        {
            // var users = await _context.Customers.Where(user => user.AccountNumber == AccountNumber).FirstOrDefaultAsync();
            var users = await _context.Customers.Where(user => user.AccountNumber == AccountNumber).FirstOrDefaultAsync();
            if (users == null)
                throw new Exception($"AccountNumber  doesn't exist");

            users.MobileNumber = customerDetails.MobileNumber;

            await _context.SaveChangesAsync();

            return users;
        }




        public async Task<CustomerDetails> DeleteCustomerAsync(long AccountNumber)
        {
               var users =await _context.Customers.Where(user => user.AccountNumber == AccountNumber).FirstOrDefaultAsync();

            if (users == null)
                throw new Exception($"AccountNumber {AccountNumber} doesn't exist");
             
            _context.Customers.Remove(users);
             await _context.SaveChangesAsync();
            return users;
        }


     }
}






























//public async Task<List<CustomerDetails>> GetCustomerAsync()
//{

//    return await _context.Customers.ToListAsync();

//}

//public async Task<List<CustomerDetails>> GetAllCustomerNameAsync( )
//{
//    return await _context.Customers.ToListAsync();
//}

















//public async Task<bool> CreateCustomerDetailsAsync(CustomerDetails customerDetails)
//{
//    await _context.Customers.AddAsync(customerDetails);
//    await _context.SaveChangesAsync();

//    return true;
//}







//public async Task<CustomerDetails> CreateCustomerAsync(CustomerDetails customerDetails)
//{
//    await _context.Customers.AddAsync(customerDetails);
//    await _context.SaveChangesAsync();

//    return customerDetails;
//}



//public async Task<CustomerDetails> UpdateCustomerAsync(long AccountNumber, CustomerDetails customerDetails)
//        {
//              var users =await _context.Customers.Where(user => user.AccountNumber == AccountNumber).FirstOrDefaultAsync();

//            if (users == null)
//                throw new Exception($"AccountNumber {AccountNumber} doesn't exist");

//            users.FirstName = customerDetails.FirstName;
//            users.LastName = customerDetails.LastName;
//            users.Address = customerDetails.Address;
//            users.City = customerDetails.City;
//            users.MobileNumber = customerDetails.MobileNumber;
//            users.AccountHolder = customerDetails.AccountHolder;
//            users.AccountNumber = customerDetails.AccountNumber;
//            users.BankName = customerDetails.BankName;
//            users.IFSCCode = customerDetails.IFSCCode;
//            users.Branch = customerDetails.Branch;


//            await _context.SaveChangesAsync();

//            return users;
//        }






//public async Task<bool> UpdateCustomerPersonalDetailsAsync(long AccountNumber, CustomerDetails customerDetails)
//{
//    var users = await _context.Customers.Where(user => user.AccountNumber == AccountNumber).FirstOrDefaultAsync();

//    if (users == null)
//        throw new Exception($"AccountNumber {AccountNumber} doesn't exist");


//    users.Address = customerDetails.Address;
//    users.City = customerDetails.City;
//    users.MobileNumber = customerDetails.MobileNumber;
//    users.AccountHolder = customerDetails.AccountHolder;
//    users.CreatedDate = customerDetails.CreatedDate;
//    users.BankName = customerDetails.BankName;
//    users.IFSCCode = customerDetails.IFSCCode;
//    users.Branch = customerDetails.Branch;


//    await _context.SaveChangesAsync();

//    return true;
//}




//public async Task<CustomerDetails> CreateAsync(CustomerDetails customerDetails)
//{

//    await _context.Customers.AddAsync(customerDetails);
//    await _context.SaveChangesAsync();

//    return customerDetails;
//}
//public async Task<CustomerDetails> CreateCustomerDetailsAsync(CreateCustomer customer)
//{
//   var res = await _context.Customers.AddAsync(customer);
//    var result =await _context.SaveChangesAsync(res);

//    return result;
//}


//public async Task<CustomerDetails> UpdateCustomerBankDetailsByIdAsync(int id, CustomerDetails customerDetails)
//{
//    var users = await _context.Customers.Where(user => user.CustomerId == id).FirstOrDefaultAsync();

//    if (users == null)
//        throw new Exception($"Customer {id} doesn't exist");

//    users.AccountHolder = customerDetails.AccountHolder;
//    users.AccountNumber = customerDetails.AccountNumber;
//    users.AccountHolder = customerDetails.AccountHolder;
//    users.IFSCCode = customerDetails.IFSCCode;
//    users.BankName = customerDetails.BankName;
//    users.Branch = customerDetails.Branch;
//    await _context.SaveChangesAsync();

//    return users;
//}




//public CustomerDetails GetCustomerById(int id)
//{

//    var customer = _context.Customers.Where(us => us.CustomerId == id).FirstOrDefault();

//    return customer;

//}
//public List<CustomerDetails> GetCustomer()
//{
//    var customer = _context.Customers.ToList();
//    return customer;

//}


//public bool CreateCustomer(CustomerDetails customerDetails)
//{
//    _context.Customers.Add(customerDetails);
//    _context.SaveChanges();

//    return true;
//}

//public bool UpdateCustomer(int id, CustomerDetails customerDetails)
//{
//    var users = _context.Customers.Where(user => user.CustomerId == id).FirstOrDefault();
//    //var users = (from el in _context.Customers
//    //                           where el.CustomerId == id
//    //                           select el).FirstOrDefault();

//    users.CustomerId = customerDetails.CustomerId;
//    users.FirstName = customerDetails.FirstName;
//    users.LastName = customerDetails.LastName;
//    users.AccountHolder = customerDetails.AccountHolder;
//    users.AccountNumber = customerDetails.AccountNumber;
//    users.AccountHolder = customerDetails.AccountHolder;
//    users.IFSCCode = customerDetails.IFSCCode;
//    users.BankName = customerDetails.BankName;
//    users.Branch = customerDetails.Branch;
//    users.MobileNumber = customerDetails.MobileNumber;
//    _context.SaveChanges();

//    return true;
//}

//public bool DeleteCustomer(int id)
//{
//    var users = _context.Customers.Where(user => user.CustomerId == id).FirstOrDefault();

//    //var users = (from el in _context.Customers
//    //                           where el.CustomerId == id
//    //                           select el).FirstOrDefault();


//    _context.Customers.Remove(users);
//    _context.SaveChanges();
//    return true;
//}







//public List<SelectCustomerDetails> GetCustomer()
//{
//    var employeeList =
//    (from e in _context.Customers
//     select new SelectCustomerDetails
//     {
//         Id = e.CustomerId,
//         FirstName = e.FirstName,
//          LastName = e.LastName,
//           BankName = e.BankName,
//            Branch = e.Branch,
//             MobileNumber = e.MobileNumber
//     }).ToList();
//    return employeeList;
//}




//#region Private Variables
//private readonly DataContext _context;
//#endregion

//#region Constructors
//public CustomerRepository(DataContext context)
//{
//    _context = context;
//}
//#endregion

//#region Public Methods
//public CustomerDetails GetCustomerById(int id)
//{
//    var customer = (from e in _context.Customers
//                    where e.Id == id
//                    select new CustomerDetails
//                    {
//                        Id = e.Id,
//                        FirstName = e.FirstName,
//                        LastName = e.LastName,
//                        AccountHolder = e.AccountHolder,
//                        AccountNumber = e.AccountNumber,
//                        IFSCCode = e.IFSCCode,
//                        BankName = e.BankName,
//                        Branch = e.Branch,
//                        MobileNumber = e.MobileNumber
//                    }).FirstOrDefault();
//    return customer;
//}

//public List<CustomerDetails> GetCustomer()
//{
//    var employeeList = (from e in _context.Customers
//                        select new CustomerDetails
//                        {
//                            Id = e.Id,
//                            FirstName = e.FirstName,
//                            LastName = e.LastName,
//                            AccountHolder = e.AccountHolder,
//                            AccountNumber = e.AccountNumber,
//                            IFSCCode = e.IFSCCode,
//                            BankName = e.BankName,
//                            Branch = e.Branch,
//                            MobileNumber = e.MobileNumber
//                        }).ToList();
//    return employeeList;


//}

////public Employee GetEmployeeByName(string name)
////{
////    var employee = (from e in context.Employees
////                    where e.Name.ToLower().Equals(name.ToLower())
////                    select new Employee
////                    {
////                        Id = e.Id,
////                        Name = e.Name,
////                        DateOfBirth = e.DateOfBirth,
////                        DateOfJoining = e.DateOfJoining,
////                        Email = e.Email,
////                        Competency = e.Competency.Name,
////                        Department = e.Department.Name,
////                        CompetencyId = e.Competency.Id,
////                        DepartmentId = e.Department.Id
////                    }).FirstOrDefault();
////    return employee;
////}

//public bool CreateCustomer(CustomerDetails customer)
//{
//    var checkEmployeeExists = (from el in _context.Customers
//                               where el.Id.Equals(customer.Id)
//                               select el).Count();

//    if (checkEmployeeExists > 0)
//        throw new Exception("Employee already exists!");

//    var newEmployee = new CustomerDetails
//    {
//        Id = customer.Id,
//        FirstName = customer.FirstName,
//        LastName = customer.LastName,
//        AccountHolder = customer.AccountHolder,
//        AccountNumber = customer.AccountNumber,
//        IFSCCode = customer.IFSCCode,
//        BankName = customer.BankName,
//        Branch = customer.Branch,
//        MobileNumber = customer.MobileNumber
//    };

//    ////Adding the employee in the list
//    //_context.Customers.Add(newEmployee);
//    //_context.SaveChanges();

//    //return true;

//    _context.Customers.Add(newEmployee);
//    _context.SaveChanges();
//    return true;
//}

//public bool UpdateCustomer(int id, CustomerDetails customer)
//{
//    var customerToBeUpdated = (from el in _context.Customers
//                               where el.Id == id
//                               select el).FirstOrDefault();

//    if (customerToBeUpdated == null)
//        throw new Exception($"CustomerDetails {id} doesn't exist");

//    customerToBeUpdated.FirstName = customer.FirstName;
//    customerToBeUpdated.LastName = customer.LastName;
//    customerToBeUpdated.AccountHolder = customer.AccountHolder;
//    customerToBeUpdated.AccountNumber = customer.AccountNumber;
//    customerToBeUpdated.IFSCCode = customer.IFSCCode;
//    customerToBeUpdated.BankName = customer.BankName;
//    customerToBeUpdated.Branch = customer.Branch;
//    customerToBeUpdated.MobileNumber = customer.MobileNumber;

//    _context.SaveChanges();
//    return true;
//}

//public bool DeleteCustomer(int id)
//{
//    var customers = (from el in _context.Customers
//                     where el.Id == id
//                     select el).FirstOrDefault();

//    if (customers == null)
//        throw new Exception($"CustomerDetails {id} doesn't exist");

//    _context.Customers.Remove(customers);
//    _context.SaveChanges();

//    return true;
//}

//public CustomerDetails GetEmployees()
//{
//    throw new NotImplementedException();
//}
//#endregion