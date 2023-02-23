using AutoMapper;
using BankManagementSystem.Controllers;
using BankManagementSystem.Domain.Enum;
using BankManagementSystem.Mappings;
using BankManagementSystem.models;
using BankManagementSystem.Servicess.InterfaceService;
using BankManagementSystem.Servicess.Models;
using BankManagementSystem.Servicess.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.UnitTest
{
    public class FundControllerUnitTest
    {
        private readonly Mock<IMoneyTransactionService> _MoneyTransactionService;
        private readonly IMapper _mapper;
        private readonly FundController _fundController;

        public FundControllerUnitTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApiMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _MoneyTransactionService = new Mock<IMoneyTransactionService>();
            _fundController = new FundController(_MoneyTransactionService.Object, _mapper);
        }

        [Fact]

        public async Task GetFundTransactionById_Success()
        {
            //arrange
            var data = new List<GetMoneyTransById>
            {
                new GetMoneyTransById
                {
                    TransactionId = 1,
                    Amount = 10000,
                    DestinationAccountNumber = 13339,
                    TransactionType = TransactionTypes.Deposit,
                    TransactionDate = DateTime.Now,
                    AccountNumber = 1393543,
                }
            };

            _MoneyTransactionService.Setup(x => x.GetMoneyTransactionsById(It.IsAny<long>())).ReturnsAsync(data);

            //act

            var response = await _fundController.GetMoneyTransactionsById(It.IsAny<long>());

            //assert

            Assert.IsType<ActionResult<List<GetFundTransactionById>>>(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;

            Assert.IsType<List<GetFundTransactionById>>(result.Value);
            var details = result.Value as List<GetFundTransactionById>;

        }

        [Fact]

        public async Task GetFundTransactionById_NoContent()
        {
            //arrange
            List<GetMoneyTransById> data = null;

            _MoneyTransactionService.Setup(x => x.GetMoneyTransactionsById(It.IsAny<long>())).ReturnsAsync(data);

            //act

            var response = await _fundController.GetMoneyTransactionsById(It.IsAny<long>());

            //Assert

            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]

        public async Task GetFundTransactionById_InternalServerError()
        {
            //arrange

            _MoneyTransactionService.Setup(x => x.GetMoneyTransactionsById(It.IsAny<long>())).Throws(new Exception());

            //act

            var response = await _fundController.GetMoneyTransactionsById(It.IsAny<long>());

            //Assert

            Assert.Equal("500", ((ObjectResult)response.Result).StatusCode.ToString());
        }

        [Fact]

        public async Task CreateTransaction_Success()
        {
            //arrange

            var data = new MoneyTransaction()
            {
                TransactionId = 1,
                DestinationAccountNumber = 1938222,
                Amount = 10000,
                Balance = 2000,
                TransactionType = TransactionTypes.Deposit,
                TransactionDate = DateTime.Now,
                AccountNumber = 1393543,

            };
            _MoneyTransactionService.Setup(x => x.PostTransactionAsync(It.IsAny<MoneyTransaction>())).ReturnsAsync(data);
            //act
            var details = await _fundController.PostTransactionAsync(It.IsAny<PostFundTransaction>());
            //aasert
            Assert.IsType<OkObjectResult>(details);
            var result = details as OkObjectResult;

            Assert.Equal("Successful Transaction", result.Value);

        }

        [Fact]

        public async Task CreateTransaction_InternalServerError()
        {
            //arrange

            _MoneyTransactionService.Setup(x => x.PostTransactionAsync(It.IsAny<MoneyTransaction>())).Throws(new Exception());

            //act

            var data = await _fundController.PostTransactionAsync(It.IsAny<PostFundTransaction>());

            //assert
            var result = data as ObjectResult;
            Assert.Equal("500", ((ObjectResult)data).StatusCode.ToString());

        }
    }
}
