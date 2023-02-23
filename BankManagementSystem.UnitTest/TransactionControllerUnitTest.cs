using AutoMapper;
using Azure;
using BankManagementSystem.Controllers;
using BankManagementSystem.Domain.Enum;
using BankManagementSystem.Domain.Model;
using BankManagementSystem.Mappings;
using BankManagementSystem.Model;
using BankManagementSystem.models;
using BankManagementSystem.Servicess.InterfaceService;
using BankManagementSystem.Servicess.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute.Routing.Handlers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.UnitTest
{
    public class TransactionControllerUnitTest
    {
        private readonly Mock<ITransactionService> _TransactionService;
        private readonly IMapper _mapper;
        private readonly TransactionController _transactionController; 


        public TransactionControllerUnitTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApiMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _TransactionService = new Mock<ITransactionService>();
            _transactionController = new TransactionController(_TransactionService.Object, _mapper);
        }

        private List<TransactionDetails> GetTransaction()
        {
            List<TransactionDetails> result = new List<TransactionDetails>()
            {
                new TransactionDetails
                {
                    TransactionId =1,
                    Amount =10000,
                    Balance =2000,
                    TransactionType = TransType.Deposit,
                    TransactionDate = DateTime.Now,
                    AccountNumber = 1393,
                   

                }
            };
            return result;
        }
        [Fact]

        public async Task GetTransaction_success()
        {
            //arrange

            _TransactionService.Setup(x => x.GetTransactionsByIdAsync()).ReturnsAsync(GetTransaction());

            //act
            var response = await _transactionController.GetTransactionsByIdAsync();

            //assert
            Assert.IsType<ActionResult<List<TransactionDetails>>>(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;

            Assert.IsType<List<TransactionDetails>>(result.Value);

            var Item = result.Value as List<TransactionDetails>;

            Assert.Equal(GetTransaction().Count,Item.Count);
        }

        [Fact]

        public async Task GetTransaction_NoContentResult()
        {
            //arrage
            var data = new List<TransactionDetails>
            {
            };
            _TransactionService.Setup(x => x.GetTransactionsByIdAsync()).ReturnsAsync(data);

            //act
            var response = await _transactionController.GetTransactionsByIdAsync();

            //Assrt

            Assert.IsType<NoContentResult>(response.Result);    
            var result = response.Result as NoContentResult;    

            Assert.Equal("204",result.StatusCode.ToString());    

        }

        [Fact]

        public async Task GetTransaction_InternalServerError()
        {
            //arrange

            _TransactionService.Setup(x => x.GetTransactionsByIdAsync()).Throws(new Exception());

            //act

            var details = await _transactionController.GetTransactionsByIdAsync();

            //Assert

            Assert.Equal("500",((ObjectResult)details.Result).StatusCode.ToString());   
        }



        [Fact]

        public async Task GetTransactionDetailsById_Success()
        {
            //Arrange
            var data = new List<TransactionModel>
            {
                new TransactionModel
                {
                    TransactionId = 1,
                    Amount = 10000,
                    Balance =2000,
                    TransactionType = TransType.Deposit,
                    TransactionDate = DateTime.Now,
                    AccountNumber = 1393543,
                }

            };
            _TransactionService.Setup(x => x.GetTransactionsDetailsAsync(It.IsAny<long>())).ReturnsAsync(data);

            //Act
            var details = await _transactionController.GetTransactionsDetailsAsync(It.IsAny<long>());

            //Assert

            Assert.IsType<ActionResult<List<TransactionPersonalDetails>>>(details);
            Assert.IsType<OkObjectResult>(details.Result);

            var response = details.Result as OkObjectResult;

            Assert.IsType<List<TransactionPersonalDetails>>(response.Value);
            var responseresult = response.Value as List<TransactionPersonalDetails>;

            //Assert.Equal(10000, responseresult.Amount);
        }

        [Fact]

        public async Task GetTransactionDetailsById_204()
        {
            //Arrange
            
            List<TransactionModel> transactionModelList = null;
 
            _TransactionService.Setup(x => x.GetTransactionsDetailsAsync(It.IsAny<long>())).ReturnsAsync(transactionModelList);

            //Act
            var details = await _transactionController.GetTransactionsDetailsAsync(It.IsAny<long>());

            //Assert

            Assert.IsType<NoContentResult>(details.Result);

        }

        [Fact]

        public async Task GetTransactionDetailsById_InternalServerError()
        {
            //arrange
            _TransactionService.Setup(x => x.GetTransactionsDetailsAsync(4)).Throws(new Exception());

            //act
            var details = await _transactionController.GetTransactionsDetailsAsync(4);

            //assert
            Assert.Equal("500", ((ObjectResult)details.Result).StatusCode.ToString());
        }

        [Fact]

        public async Task GetLastTwoTransactionDetailsById_Success()
        {
            //Arrange
            var data = new List<TransactionModel>
            {
                new TransactionModel
                {
                    TransactionId = 1,
                    Amount = 10000,
                    Balance =2000,
                    TransactionType = TransType.Deposit,
                    TransactionDate = DateTime.Now,
                    AccountNumber = 1393543,
                }


            };
            _TransactionService.Setup(x => x.GetLastTwoTransactionsByIdAsync(It.IsAny<long>())).ReturnsAsync(data);

            //Act
            var details = await _transactionController.GetLastTwoTransactionsByIdAsync(It.IsAny<long>());

            //Assert

            Assert.IsType<ActionResult<List<TransactionPersonalDetails>>>(details);
            Assert.IsType<OkObjectResult>(details.Result);

            //var response = details.Result as OkObjectResult;

            //Assert.IsType<List<TransactionPersonalDetails>>(response.Value);
            //var responseresult = response.Value as TransactionPersonalDetails;

            //Assert.Equal(10000, responseresult.Amount);
        }

        [Fact]

        public async Task GetLastTwoTransactionDetailsById_204()
        {
            //Arrange
          
            List<TransactionModel> data = null;
            _TransactionService.Setup(x => x.GetLastTwoTransactionsByIdAsync(It.IsAny<long>())).ReturnsAsync(data);

            //Act
            var details = await _transactionController.GetLastTwoTransactionsByIdAsync(It.IsAny<long>());

            //Assert

            Assert.IsType<NoContentResult>(details.Result);

        }

        [Fact]

        public async Task GetLastTwoTransactionDetailsById_InternalServerError()
        {
            //arrange
            _TransactionService.Setup(x => x.GetLastTwoTransactionsByIdAsync(It.IsAny<long>())).Throws(new Exception());

            //act
            var details = await _transactionController.GetLastTwoTransactionsByIdAsync(It.IsAny<long>());

            //assert
            Assert.Equal("500", ((ObjectResult)details.Result).StatusCode.ToString());
        }

        [Fact]

        public async Task CreateTransaction_Success()
        {
            //arrange

            var data = new Transaction()
            {

                Amount = 10000,
                Balance = 2000,
                TransactionType = TransType.Deposit,
                TransactionDate = DateTime.Now,
                AccountNumber = 1393543,

            };
            _TransactionService.Setup(x => x.PostTransactionAsync(It.IsAny<Transaction>())).ReturnsAsync(data);
            //act
            var details = await _transactionController.PostTransactionAsync(It.IsAny<PostTransactionDetails>());
            //aasert
            Assert.IsType<OkObjectResult>(details);
            var result = details as OkObjectResult;

            Assert.Equal("Successful Transaction", result.Value);

        }

        [Fact]

        public async Task CreateTransaction_InternalServerError()
        {
            //arrange

            _TransactionService.Setup(x => x.PostTransactionAsync(It.IsAny<Transaction>())).Throws(new Exception());

            //act

            var data = await _transactionController.PostTransactionAsync(It.IsAny<PostTransactionDetails>());

            //assert
            var result = data as ObjectResult;
            Assert.Equal("500", ((ObjectResult)data).StatusCode.ToString());

        }


    }
}
