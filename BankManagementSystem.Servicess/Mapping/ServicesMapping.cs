using BankManagementSystem.Domain.Model;
using BankManagementSystem.Model;
using BankManagementSystem.Servicess.Models;
using BankManagementSystem.Servicess.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BankManagementSystem.Servicess.Mapping
{
    public class ServicesMapping : Profile
    {
        public ServicesMapping()
        {
            CreateMap<CustomerDetails, GetAllCustomerDetailsId>();

            CreateMap<CustomerDetails, GetAllPersonalCustomerDetails>();

            CreateMap<CustomerModel, CustomerDetails>().ReverseMap();


            CreateMap<CustomerDetails, CustomerModel>().ReverseMap();

            CreateMap<CustomerDetails, UpdateMobileNumber>().ReverseMap();


            CreateMap<Transaction, TransactionDetails>().ReverseMap();

            CreateMap<TransactionDetails, TransactionModel>();


           CreateMap<MoneyTransaction, MoneyTransactionDetails>().ReverseMap();

            CreateMap<MoneyTransactionDetails, GetMoneyTransById>();

        }
      
    }
}
