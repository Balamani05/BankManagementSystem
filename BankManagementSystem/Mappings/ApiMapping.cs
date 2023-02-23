using AutoMapper;
using BankManagementSystem.Ado.Services.Model;
//using BankManagementSystem.Ado.Services.Model;
using BankManagementSystem.Model;
using BankManagementSystem.models;
using BankManagementSystem.Servicess.Models;


namespace BankManagementSystem.Mappings
{
    public class ApiMapping : Profile
    {
        public ApiMapping()
        {

            CreateMap<GetAllPersonalCustomerDetails, GetAllPersonalDetails>();

            CreateMap<GetAllCustomerDetailsId, GetAllPersonalDetails>();



            CreateMap<GetAllCustomerDetailsId, GetCustomerBankDetails>();


            CreateMap<GetAllCustomerDetailsId, GetCurrentBalance>();

            CreateMap<PostCustomerDetails, CustomerModel>();

            CreateMap<PutCustomerDetailsId, CustomerAllDetails>();




            CreateMap<PutCustomerMobileNumber, UpdateMobileNumber>();

            CreateMap<PostTransactionDetails, Transaction>();

            CreateMap<TransactionModel, TransactionPersonalDetails>();


            CreateMap<PostFundTransaction, MoneyTransaction>();

           // CreateMap<GetFundTransactionById, GetMoneyTransById>();

            CreateMap<GetMoneyTransById, GetFundTransactionById>();


        }
    }
}




































//CreateMap<CustomerAllDetails, GetAllPersonalDetails>();

//CreateMap<CustomerAllDetails, GetCustomerDetails>();

//CreateMap<PostCustomerDetails, CustomerAllDetails>();

//CreateMap<PutCustomerDetailsId, CustomerAllDetails>();

//CreateMap<PostsCustomersDetails, CustomerAllDetails>();