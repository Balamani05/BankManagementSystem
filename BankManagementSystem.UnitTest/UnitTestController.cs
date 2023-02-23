using AutoMapper;
using Azure;
using BankManagementSystem.Controllers;
using BankManagementSystem.Dto.Interface;
using BankManagementSystem.Mappings;
using BankManagementSystem.Model;
using BankManagementSystem.models;
using BankManagementSystem.Servicess.InterfaceService;
using BankManagementSystem.Servicess.Models;
using BankManagementSystem.Servicess.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Moq;
using NSubstitute;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using Xunit;


namespace BankManagementSystem.UnitTest
{

    public class UnitTestController
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly IMapper _mapper;
        private readonly CustomerController _customerController;

        private CustomerModel customerdata = new CustomerModel();
        private PostCustomerDetails postcustomerdata = new PostCustomerDetails();


        public UnitTestController()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApiMapping());
            });
            _mapper = mockMapper.CreateMapper();
            _customerServiceMock = new Mock<ICustomerService>();
            _customerController = new CustomerController(_customerServiceMock.Object, _mapper);
        }

        private List<GetAllCustomerDetailsId> GetCustomer()
        {
            List<GetAllCustomerDetailsId> customerData = new List<GetAllCustomerDetailsId>
                     {
                        new GetAllCustomerDetailsId
                        {
                            AccountNumber = 139328100,
                            FirstName = "bala",
                            LastName  = "m",
                            Address = "davf",
                            City = "chennai",
                            MobileNumber = "74737",
                            AccountHolder = "BALA M",
                            IFSCCode = "ioba3728",
                            BankName = "IOB",
                            Branch = "chennai",
                            CurrentBalance = 2000

                                },
                            };
            return customerData;
        }


        private List<CustomerDetails> GetCustomerDetails()
        {
            List<CustomerDetails> customerData = new List<CustomerDetails>
                     {
                        new CustomerDetails
                        {
                            AccountNumber = 139328100,
                            FirstName = "bala",
                            LastName  = "m",
                            Address = "davf",
                            City = "chennai",
                            MobileNumber = "74737",
                            AccountHolder = "BALA M",
                            IFSCCode = "ioba3728",
                            BankName = "IOB",
                            Branch = "chennai",
                            CurrentBalance = 2000

                                },
                            };
            return customerData;
        }

        [Fact]
        public async Task GetCustomerAsync_Success()
        {
            //arrange
            var customerList = new List<GetAllPersonalCustomerDetails>
            { new GetAllPersonalCustomerDetails
                {
                                    
                                            FirstName = "bala",
                                            LastName = "m",
                                            Address = "davf",
                                            City = "chennai",
                                            MobileNumber = "74737",
 
                }
            };
            _customerServiceMock.Setup(x => x.GetCustomerAsync()).ReturnsAsync(customerList);
            //act
            var response =await _customerController.GetCustomerAsync();

            //assert
            Assert.IsType<ActionResult<List<GetAllPersonalDetails>>>(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;

            Assert.IsType<List<GetAllPersonalDetails>>(result.Value);

            var Item = result.Value as List<GetAllPersonalDetails>;

            Assert.Equal(customerList.Count, Item.Count);

            //assert
            //Assert.True(response.IsCompletedSuccessfully);
            //var result = response.Result;
            //Assert.IsType<ActionResult<List<GetAllCustomerName>>>(result);
            //var actualResult = (List<GetAllCustomerName>)((OkObjectResult)result.Result!).Value!;


        }



        [Fact]
        public async Task GetReturnStatus204()
        {

            //arrange
            var customerList = new List<GetAllPersonalCustomerDetails>
            {

            };
            _customerServiceMock.Setup(x => x.GetCustomerAsync()).ReturnsAsync(customerList);

            //Act

            var response = await _customerController.GetCustomerAsync();

            //Assert

            Assert.IsType<NoContentResult>(response.Result);

            var cus = response.Result as NoContentResult;

            Assert.Equal("204", (cus).StatusCode.ToString());


        }

        [Fact]
        public async Task GetAllCustomer_ShouldReturn_500()
        {
            //arrage

            _customerServiceMock.Setup(x => x.GetCustomerAsync()).Throws(new Exception());

            //act
            var Customerresult = await _customerController.GetCustomerAsync();

            //assert
            Assert.Equal("500", ((ObjectResult)Customerresult.Result).StatusCode.ToString());
        }

        [Fact]
        public async Task GetCustomerByIdAsync_Success()
        {
            //arrange
            //_customerServiceMock.Setup(x => x.GetCustomerBankByIdAsync(1)).ReturnsAsync(customerList[0]);
            _customerServiceMock.Setup(x => x.GetCustomerBankByIdAsync(1)).ReturnsAsync(GetCustomer()[0]);
            //act
            var response = await _customerController.GetCustomerBankByIdAsync(1);

            //assert
            Assert.IsType<ActionResult<GetCustomerBankDetails>>(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;

            Assert.IsType<GetCustomerBankDetails>(result.Value);

            var Item = result.Value as GetCustomerBankDetails;

            Assert.Equal("BALA M", Item.AccountHolder);
        }

        [Fact]

        public async Task GetById_ShouldReturn_204()
        {
            //arrange 

            //_customerServiceMock.Setup(m => m.GetCustomerBankByIdAsync(1)).ReturnsAsync(customerList[0]);

            _customerServiceMock.Setup(x => x.GetCustomerBankByIdAsync(1)).ReturnsAsync(GetCustomer()[0]);


            //act
            var result = await _customerController.GetCustomerBankByIdAsync(4);

            //assert

            Assert.IsType<NoContentResult>(result.Result);

        }

        [Fact]
        public async Task GetById_ShouldReturn_500()
        {
            //arrage

            _customerServiceMock.Setup(x => x.GetCustomerBankByIdAsync(4)).Throws(new Exception());


            //act
            var Customerresult = await _customerController.GetCustomerBankByIdAsync(4);

            //assert
            Assert.Equal("500", ((ObjectResult)Customerresult.Result).StatusCode.ToString());
        }


        [Fact]
        public async Task GetCustomerPersonalByIdAsync_Success()
        {
            //arrange
            _customerServiceMock.Setup(x => x.GetCustomerDetailsByIdAsync(1)).ReturnsAsync(GetCustomer()[0]);
            //act
            var response = await _customerController.GetCustomerDetailsByIdAsync(1);

            //assert
            Assert.IsType<ActionResult<GetAllPersonalDetails>>(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;

            Assert.IsType<GetAllPersonalDetails>(result.Value);

            var Item = result.Value as GetAllPersonalDetails;

            Assert.Equal("bala", Item.FirstName);
        }

        [Fact]

        public async Task GetCustomerById_ShouldReturn_204()
        {
            //arrange 

            //_customerServiceMock.Setup(m => m.GetCustomerBankByIdAsync(1)).ReturnsAsync(customerList[0]);

            _customerServiceMock.Setup(x => x.GetCustomerDetailsByIdAsync(1)).ReturnsAsync(GetCustomer()[0]);


            //act
            var result = await _customerController.GetCustomerDetailsByIdAsync(4);

            //assert

            Assert.IsType<NoContentResult>(result.Result);

        }

        [Fact]
        public async Task GetCustomerById_ShouldReturn_500()
        {
            //arrage

            _customerServiceMock.Setup(x => x.GetCustomerDetailsByIdAsync(4)).Throws(new Exception());


            //act
            var Customerresult = await _customerController.GetCustomerDetailsByIdAsync(4);

            //assert
            Assert.Equal("500", ((ObjectResult)Customerresult.Result).StatusCode.ToString());
        }


        [Fact]
        public async Task GetCurrentAccountByIdAsync_Success()
        {
            //arrange
            _customerServiceMock.Setup(x => x.GetCurrentBalanceByIdAsync(1)).ReturnsAsync(GetCustomer()[0]);
            //act
            var response = await _customerController.GetCurrentBalanceByIdAsync(1);

            //assert
            Assert.IsType<ActionResult<GetCurrentBalance>>(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var result = response.Result as OkObjectResult;

            Assert.IsType<GetCurrentBalance>(result.Value);

            var Item = result.Value as GetCurrentBalance;

            Assert.Equal(2000, Item.CurrentBalance);
        }

        [Fact]

        public async Task GetCurrentAccountById_ShouldReturn_204()
        {
            //arrange 

            //_customerServiceMock.Setup(m => m.GetCustomerBankByIdAsync(1)).ReturnsAsync(customerList[0]);

            _customerServiceMock.Setup(x => x.GetCurrentBalanceByIdAsync(1)).ReturnsAsync(GetCustomer()[0]);


            //act
            var result = await _customerController.GetCurrentBalanceByIdAsync(4);

            //assert

            Assert.IsType<NoContentResult>(result.Result);

        }

        [Fact]
        public async Task GetCurrentAccountById_ShouldReturn_500()
        {
            //arrage

            _customerServiceMock.Setup(x => x.GetCurrentBalanceByIdAsync(4)).Throws(new Exception());


            //act
            var Customerresult = await _customerController.GetCurrentBalanceByIdAsync(4);

            //assert
            Assert.Equal("500", ((ObjectResult)Customerresult.Result).StatusCode.ToString());
        }
        private CustomerModel GetCustomersData()
        {
            customerdata = new CustomerModel
            {

                FirstName = "bala",
                LastName = "m",
                Address = "davf",
                City = "chennai",
                MobileNumber = "74737",
                AccountHolder = "BALA M",
                IFSCCode = "ioba3728",
                BankName = "IOB",
                Branch = "chennai",

            };

            return customerdata;
        }
        private PostCustomerDetails PostCustomers()
        {
            postcustomerdata = new PostCustomerDetails
            {


                FirstName = "bala",
                LastName = "m",
                Address = "davf",
                City = "chennai",
                MobileNumber = "74737",
                AccountHolder = "BALA M",
                IFSCCode = "ioba3728",
                BankName = "IOB",
                Branch = "chennai",

            };

            return postcustomerdata;
        }

        [Fact]

        public async Task postcustomer_ShouldResult_200()
        {
            //arrange

            _customerServiceMock.Setup(x => x.CreateCustomerAsync(GetCustomersData())).ReturnsAsync(GetCustomersData());


            //act
            var actualresult = await _customerController.CreateCustomerAsync(PostCustomers());

            //Assert

            Assert.IsType<OkObjectResult>(actualresult);

            var result = actualresult as OkObjectResult;
            Assert.Equal("Successfull create a Account", result.Value);
        }

        [Fact]
        public async Task PostCustomer_ShouldReturn_400()
        {
            //arrage
            var data = new CustomerModel()
            {
                FirstName = null,
                LastName = "m",
                Address = "davf",
                City = "chennai",
                MobileNumber = "74737",
                AccountHolder = "BALA M",
                IFSCCode = "ioba3728",
                BankName = "IOB",
                Branch = "chennai",
            };
            var Details = new PostCustomerDetails()
            {
                FirstName = null,
                LastName = "m",
                Address = "davf",
                City = "chennai",
                MobileNumber = "74737",
                AccountHolder = "BALA M",
                IFSCCode = "ioba3728",
                BankName = "IOB",
                Branch = "chennai",

            };


            _customerServiceMock.Setup(x => x.CreateCustomerAsync(data)).ReturnsAsync(data);


            //act
            var Customerresult = await _customerController.CreateCustomerAsync(Details);

            //assert

            //Assert.IsType<ObjectResult>(Customerresult.Result);
            //var response = Customerresult.Result as ObjectResult;

            Assert.IsType<ObjectResult>(Customerresult);
            var result = Customerresult as ObjectResult;

            var Actualresult = result.Value as ProblemDetails;

            Assert.Equal("400", Actualresult.Status.ToString());
            Assert.Equal("Customer Name is empty!", Actualresult.Detail.ToString());


        }

        [Fact]
        public async Task PostCustomer_ShouldReturn_500()
        {
            //arrage

            _customerServiceMock.Setup(x => x.CreateCustomerAsync(It.IsAny<CustomerModel>())).Throws(new Exception());


            //act
            var Customerresult = await _customerController.CreateCustomerAsync(It.IsAny<PostCustomerDetails>());

            //assert
            var response = Customerresult as ObjectResult;

            //Assert.Equal("500", response.Value.ToString());
            Assert.Equal("500", ((ObjectResult)Customerresult).StatusCode.ToString());

            //Assert.Equal(StatusCodes.Status500InternalServerError, Result.StatusCode);
        }

        [Fact]
        public async Task putcustomer_ShouldResult_200()
        {
            //arrange 
            var update = new PutCustomerMobileNumber()
            {

                MobileNumber = "74737",

            };
            var customer = new UpdateMobileNumber()
            {

                MobileNumber = "74737",

            };

            _customerServiceMock.Setup(m => m.UpdateCustomerMobileNumberAsync(customer, 1)).ReturnsAsync(customer);

            // act

            var result = await _customerController.UpdateCustomerMobileNumberAsync(update, 1);

            Assert.IsType<OkObjectResult>(result);
            var user = result as OkObjectResult;

            //Assert

            Assert.Equal("Successfull Updated", user.Value);

        }


        [Fact]
        public async Task PutCustomer_ShouldReturn_400()
        {
            //arrage
            var update = new PutCustomerMobileNumber()
            {

                MobileNumber = null,

            };
            var customerdata = new UpdateMobileNumber()
            {

                MobileNumber = null,

            };


            _customerServiceMock.Setup(x => x.UpdateCustomerMobileNumberAsync(customerdata, 1)).ReturnsAsync(customerdata);


            //act
            var Customerresult = await _customerController.UpdateCustomerMobileNumberAsync(update, 1);

            //assert

            //Assert.IsType<ObjectResult>(Customerresult.Result);
            //var response = Customerresult.Result as ObjectResult;

            Assert.IsType<ObjectResult>(Customerresult);
            var result = Customerresult as ObjectResult;

            var Actualresult = result.Value as ProblemDetails;

            Assert.Equal("400", Actualresult.Status.ToString());
            Assert.Equal("Customer MobileNumber is empty!", Actualresult.Detail.ToString());


        }

        [Fact]
        public async Task PutById_ShouldReturn_500()
        {
            //arrage

            _customerServiceMock.Setup(x => x.UpdateCustomerMobileNumberAsync(It.IsAny<UpdateMobileNumber>(), It.IsAny<long>())).Throws(new Exception());

            //act
            var Customerresult = await _customerController.UpdateCustomerMobileNumberAsync(It.IsAny<PutCustomerMobileNumber>(), It.IsAny<long>());

            //assert
            var result = Customerresult as ObjectResult;
            Assert.Equal("500", ((ObjectResult)result).StatusCode.ToString());
        }


        [Fact]
        public async Task DeleteCustomer_ShouldResult_200()
        {
            //arrange

            _customerServiceMock.Setup(x => x.DeleteCustomerAsync(139328100)).ReturnsAsync(GetCustomerDetails()[0]);

            //Act

            var customerResult = await _customerController.DeleteCustomerAsync(139328100);


            //Assert

            Assert.IsType<OkObjectResult>(customerResult.Result);

            var Listed = customerResult.Result as OkObjectResult;

            Assert.Equal("Customer Removed", Listed.Value);

        }

        [Fact]

        public async Task DeleteCustomer_ShouldResult_400()
        {
            //arrange

            _customerServiceMock.Setup(x => x.DeleteCustomerAsync(1)).ReturnsAsync(GetCustomerDetails()[0]);

            //act
            var result = await _customerController.DeleteCustomerAsync(5);

            //assert

            Assert.IsType<ObjectResult>(result.Result);

            var response = result.Result as ObjectResult;
            var customerresult = response.Value as ProblemDetails;

            Assert.Equal("400", customerresult.Status.ToString());
            Assert.Equal("Account number not found :5", customerresult.Detail.ToString());
        }



        [Fact]
        public async Task DeleteCustomer_ShouldReturn_500()
        {
            //arrage

            _customerServiceMock.Setup(x => x.DeleteCustomerAsync(It.IsAny<long>())).Throws(new Exception());

            //act
            var Customerresult = await _customerController.DeleteCustomerAsync(It.IsAny<long>());

            //assert

            Assert.IsType<ObjectResult>(Customerresult.Result);
            var response = Customerresult.Result as ObjectResult;
            var resultresponse = response.Value as ProblemDetails;

            Assert.Equal("Customer details cannot be deleted", resultresponse.Detail.ToString());


        }

    }












    //[Fact]
    //public async Task CanGetAllCustomer()
    //{

    //    //Arrange
    //    var customerService = new Mock<ICustomerService>();


    //    customerService.Setup(X => X.GetCustomerAsync()).ReturnsAsync(Get());
    //    var result = new CustomerController(customerService.Object, _mapper);

    //    var response = await result.GetCustomerAsync();


    //    //Assert
    //    Assert.IsType<OkObjectResult>(response.Result);

    //    var Listed = response.Result as OkObjectResult;

    //    Assert.IsType<List<CustomerModel>>(Listed.Value);

    //    var ListCustomer = Listed.Value as List<GetAllCustomerName>;

    //    Assert.Equal(Get().Count, ListCustomer.Count);


    //}













    //  
    //    private List<CustomerDetails> GetCustomer()
    //    {
    //        List<CustomerDetails> customerData = new List<CustomerDetails>
    //             {
    //                new CustomerDetails
    //                {
    //                    AccountNumber = 139328100,
    //                    FirstName = "bala",
    //                    LastName  = "m",
    //                    Address = "davf",
    //                    City = "chennai",
    //                    MobileNumber = "74737",
    //                    AccountHolder = "BALA M",
    //                    IFSCCode = "ioba3728",
    //                    BankName = "IOB",
    //                    Branch = "chennai",
    //                    CurrentBalance = 2000

    //                        },
    //                    };
    //        return customerData;
    //    }


    //    private List<CustomerDetails> GetCustomerById()
    //    {
    //        List<CustomerDetails> customer = new List<CustomerDetails>
    //        {
    //            new CustomerDetails
    //            {
    //                  AccountNumber = 1,
    //            FirstName = "bala",
    //            LastName = "m",
    //            Address = "davf",
    //            City = "chennai",
    //            CreatedDate = DateTime.Now,
    //            AccountHolder = "bala",
    //            BankName = "iob",
    //            Branch = "chennai",
    //            MobileNumber = "74737",
    //            CurrentBalance = 300,
    //            },
    //           new CustomerDetails
    //            {
    //                  AccountNumber = 2,
    //            FirstName = "bala",
    //            LastName = "m",
    //            Address = "davf",
    //            City = "chennai",
    //            CreatedDate = DateTime.Now,
    //            AccountHolder = "bala",
    //            BankName = "iob",
    //            Branch = "chennai",
    //            MobileNumber = "74737",
    //            CurrentBalance = 300,
    //            }
    //        };
    //        return customer;

    //    }



    //    private List<CustomerDetails> GetCustomerEmptyData()
    //    {
    //        List<CustomerDetails> customerData = new List<CustomerDetails>
    //        {

    //        };
    //        return customerData;
    //    }




    //    [Fact]
    //    public async Task GetCustomers()
    //    {

    //        //arrange

    //        var customerService = new Mock<ICustomerService>();
    //        // var mapper = new Mock<IMapper>();

    //        customerService.Setup(x => x.GetCustomerDataAsync()).ReturnsAsync(GetCustomer());

    //        var result = new CustomerController(customerService.Object, _mapper);

    //        //Act

    //        var customerResult = await result.GetCustomerDataAsync();


    //        //Assert

    //        Assert.IsType<OkObjectResult>(customerResult.Result);

    //        var Listed = customerResult.Result as OkObjectResult;

    //        Assert.IsType<List<CustomerDetails>>(Listed.Value);

    //        var ListCustomer = Listed.Value as List<CustomerDetails>;



    //        Assert.Equal(GetCustomer().Count, ListCustomer.Count);

    //    }


    //    [Fact]
    //    public async Task GetReturnStatus204()
    //    {

    //        //arrange

    //        var customerService = new Mock<ICustomerService>();
    //        var mapper = new Mock<IMapper>();

    //        customerService.Setup(x => x.GetCustomerDataAsync()).ReturnsAsync(GetCustomerEmptyData());

    //        var result = new CustomerController(customerService.Object, mapper.Object);

    //        //Act

    //        var customerResult = await result.GetCustomerDataAsync();


    //        //Assert

    //        Assert.IsType<NoContentResult>(customerResult.Result);

    //        var cus = customerResult.Result as NoContentResult;

    //        Assert.Equal("204", (cus).StatusCode.ToString());


    //    }

    //    [Fact]
    //    public async Task GetAllCustomer_ShouldReturn_500()
    //    {
    //        //arrage
    //        var service = new Mock<ICustomerService>();
    //        var mapper = new Mock<IMapper>();

    //        service.Setup(x => x.GetCustomerDataAsync()).Throws(new Exception());
    //        var result = new CustomerController(service.Object, mapper.Object);

    //        //act
    //        var Customerresult = await result.GetCustomerDataAsync();

    //        //assert
    //        Assert.Equal("500", ((ObjectResult)Customerresult.Result).StatusCode.ToString());
    //    }



    //    [Fact]
    //    public async Task CustomerById()
    //    {
    //        //arrange 

    //        var service = new Mock<ICustomerService>();
    //        service.Setup(m => m.GetCustomerByIdAsync(2)).ReturnsAsync(GetCustomerById()[1]);
    //        var controller = new CustomerController(service.Object, _mapper);

    //        // act

    //        var result = await controller.GetCustomerByIdAsync(2);

    //        Assert.IsType<OkObjectResult>(result.Result);
    //        var user = result.Result as OkObjectResult;

    //        //Assert
    //        Assert.Equal("200", (user).StatusCode.ToString());
    //        Assert.IsType<CustomerDetails>(user.Value);
    //        var list = user.Value as CustomerDetails;
    //        Assert.Equal(GetCustomerById()[1].AccountNumber, list.AccountNumber);
    //    }

    //    [Fact]

    //    public async Task GetById_ShouldReturn_204()
    //    {
    //        //arrange 

    //        var service = new Mock<ICustomerService>();
    //        service.Setup(m => m.GetCustomerByIdAsync(2)).ReturnsAsync(GetCustomerById()[1]);
    //        var controller = new CustomerController(service.Object, _mapper);

    //        //act
    //        var result = await controller.GetCustomerByIdAsync(4);

    //        //assert

    //        Assert.IsType<NoContentResult>(result.Result);

    //    }

    //    [Fact]
    //    public async Task GetById_ShouldReturn_500()
    //    {
    //        //arrage
    //        var service = new Mock<ICustomerService>();
    //        var mapper = new Mock<IMapper>();

    //        service.Setup(x => x.GetCustomerByIdAsync(4)).Throws(new Exception());
    //        var result = new CustomerController(service.Object, mapper.Object);

    //        //act
    //        var Customerresult = await result.GetCustomerByIdAsync(4);

    //        //assert
    //        Assert.Equal("500", ((ObjectResult)Customerresult.Result).StatusCode.ToString());
    //    }

    //


    //   





}








//[Fact]
//public async Task GetCustomerAsync_Success()
//{
//    //arrange
//    var mapp = new Mock<IMapper>();
//    var ser = new Mock<ICustomerService>();
//    var customerAllList = new List<GetAllCustomerName>
//    {
//        new GetAllCustomerName()
//        {
//               FirstName = "bala",
//                LastName  = "m",
//        }
//    };
//    ser.Setup(x => x.GetCustomerAsync());
//    mapp.Setup(X => X.Map<List<GetAllCustomerName>>(It.IsAny<List<CustomerModel>>())).Returns(customerAllList);
//    //act
//    var response = await _customerController.GetCustomerAsync();
//    //assert
//    Assert.IsType<OkObjectResult>(response.Result);



//}





































//[Fact]

//public async Task putcustomer_ShouldResult_200()
//{
//    //arrange

//    var service = new Mock<ICustomerService>();

//    service.Setup(x => x.UpdateCustomerPersonalDetailsAsync(customerdata)).ReturnsAsync(true);

//    var result = new CustomerController(service.Object, _mapper);

//    //act
//    var actualresult = await result.UpdateCustomerPersonalDetailsAsync(customerdata);

//    //Assert

//    Assert.IsType<OkObjectResult>(actualresult.Result);

//    var response = actualresult.Result as OkObjectResult;

//}

//[Fact]
//public async Task PutCustomer_ShouldReturn_500()
//{
//    //arrage
//    var service = new Mock<ICustomerService>();
//    var mapper = new Mock<IMapper>();

//    service.Setup(x => x.UpdateCustomerPersonalDetailsAsync(It.IsAny<CustomerDetails>())).Throws(new Exception());
//    var result = new CustomerController(service.Object, mapper.Object);

//    //act
//    var Customerresult = await result.UpdateCustomerPersonalDetailsAsync(It.IsAny<CustomerDetails>());

//    //assert

//    //Assert.IsType<ObjectResult>(Customerresult.Result);
//    //var response = Customerresult.Result as ObjectResult;

//    //Assert.Equal("500", response.Value.ToString());
//    Assert.Equal("500", ((ObjectResult)Customerresult.Result).StatusCode.ToString());
//}


//        private  IMapper _mapper;
//        private  ICustomerService _customerService;
//        private CustomerController _customercontroller;
//        //private GetAllPersonalDetails _getAllPersonalDetails = new GetAllPersonalDetails();
//        //private GetAllPersonalCustomerDetails _getAllPersonalCustomerDetails = new GetAllPersonalCustomerDetails();


//        public void Setup()
//        {
//            var mappingConfig = new MapperConfiguration(mc =>
//            {
//                mc.AddProfile(new ApiMapping());
//            });
//            _mapper = mappingConfig.CreateMapper();
//            _customerService = new Mock<ICustomerService>();
//            _customercontroller = new CustomerController(_customerService, _mapper);
//        }



//        private GetAllPersonalDetails ListCustomerEntity()
//        {
//            GetAllPersonalDetails _getAllPersonalDetails = new GetAllPersonalDetails
//            {

//                FirstName = "bala",
//                LastName = "m",
//                Address = "davf",
//                City = "chennai",
//                MobileNumber = "74737",

//            };

//            return _getAllPersonalDetails;
//        }
//        [Fact]
//        public void GetCustomersDetails()
//        {
//            //Arrange
//            List<GetAllPersonalCustomerDetails> _getAllPersonalCustomerDetails = new List<GetAllPersonalCustomerDetails>
//            {
//                new GetAllPersonalCustomerDetails
//                {
//                        FirstName = "bala",
//                        LastName = "m",
//                        Address = "davf",
//                        City = "chennai",
//                        MobileNumber = "74737",
//                }

//            };


//            var customer = _customerService.GetCustomerAsync().Returns(_getAllPersonalCustomerDetails);

//            //Act

//            var response = _customercontroller.GetCustomerAsync();

//            //Assert

//            var responseresult = response.Result;


//            var result = responseresult.Result as OkObjectResult;
//            var actualresult = result.Value as List<GetAllPersonalDetails>;
//            Assert.IsType<List<GetAllPersonalCustomerDetails>>(actualresult);

//            Assert.Equal(_getAllPersonalCustomerDetails.Count, actualresult.Count);


//        }





















//private List<GetAllPersonalCustomerDetails> GetCustomer()
//{
//    List<GetAllPersonalCustomerDetails> customerData = new List<GetAllPersonalCustomerDetails>
//         {
//            new GetAllPersonalCustomerDetails
//            {

//                FirstName = "bala",
//                LastName  = "m",
//                Address = "davf",
//                City = "chennai",
//                MobileNumber = "74737",


//            },
//    };
//    return customerData;
//}



//[Fact]
//public async Task GetCustomers()
//{

//    //arrange

//    var customerService = new Mock<ICustomerService>();
//    var mapper = new Mock<IMapper>();

//    customerService.Setup(x => x.GetCustomerAsync()).ReturnsAsync(GetCustomer());

//    var result = new CustomerController(customerService.Object, mapper.Object);

//    //Act

//    var customerResult = await result.GetCustomerAsync();


//    //Assert

//    Assert.IsType<OkObjectResult>(customerResult.Result);

//    var Listed = customerResult.Result as OkObjectResult;

//    Assert.IsType<List<GetAllPersonalCustomerDetails>>(Listed.Value);

//    var ListCustomer = Listed.Value as List<GetAllPersonalCustomerDetails>;



//    Assert.Equal(GetCustomer().Count, ListCustomer.Count);



//    //Assert.NotNull(customerResult);
//    //Assert.Equal(GetCustomer().ToString(), customerResult.ToString());
//    //Assert.True(customerList.Equals(customerResult));

//}











//var Listed = customerResult.Result as OkObjectResult;

//Assert.IsType<List<CustomerDetails>>(Listed.Value);

//var ListCustomer = Listed.Value as List<CustomerDetails>;



//var customer = _customerRepository.GetCustomerAsync().Returns(ListCustomerEntitys());

////act
//var response = _customerController.GetCustomerAsync();


////Assert

////Assert.IsType(response.IsCompleted);

////Assert.IsType<OkObjectResult>(response.Result);

//var responseresult = response.Result;
//Assert.IsType<List<GetAllPersonalCustomerDetails>>(_getAllPersonalCustomerDetails);
//var result = responseresult.Result as OkObjectResult;
//var actualresult = result.Value as List<GetAllPersonalDetails>;

//Assert.Equal( _getAllPersonalCustomerDetails.Count, actualresult.Count);
//Assert.IsType<OkObjectResult>(customerResult.Result);








































//[Fact]

//public async Task GetCustomerById()
//{
//    // arrange
//    var customerService = new Mock<ICustomerService>();
//    var mapper = new Mock<IMapper>();
//    var customerList = GetCustomerById1();

//    var customerReturn = GetCustomerReturn();

//    customerService.Setup(x => x.GetCustomerDetailsByIdAsync(customerList.AccountNumber)).Returns(customerReturn);


//    var controller = new CustomerController(customerService.Object, mapper.Object);

//    var result = controller.GetCustomerDetailsByIdAsync(customerList.AccountNumber);

//    var response = result.Result;

//    var actionresult = response.Value;

//    Assert.Equal(actionresult.ToString(), customerReturn.ToString());

//}

















































//}
//private List<GetAllPersonalCustomerDetails> GetCustomer()
//{
//    List<GetAllPersonalCustomerDetails> customerData = new List<GetAllPersonalCustomerDetails>
//     {
//        new GetAllPersonalCustomerDetails
//        {

//            FirstName = "bala",
//            LastName  = "m",
//            Address = "davf",
//            City = "chennai",
//            MobileNumber = "74737",


//        },

//        new GetAllPersonalCustomerDetails
//        {

//            FirstName = "bala",
//            LastName  = "m",
//            Address = "davf",
//            City = "chennai",
//            MobileNumber = "74737",


//        },
//    };
//    return customerData;
//}

//[Fact]
//public async Task GetCustomers()
//{

//    //arrange

//    var customerService = new Mock<ICustomerService>();
//    var mapper = new Mock<IMapper>();

//    mapper.Setup(x => x.Map<GetAllPersonalCustomerDetails>(It.IsAny<GetAllPersonalDetails>())).ReturnsAsync((GetAllPersonalDetails details) => new GetAllPersonalCustomerDetails() {FirstName = details.FirstName});

//    var result = new CustomerController(customerService.Object, mapper.Object);

//    //Act

//    var customerResult = await result.GetCustomerDataAsync();


//    //Assert

//    Assert.IsType<OkObjectResult>(customerResult.Result);

//    var List = customerResult.Result as OkObjectResult;

//    Assert.IsType<List<GetAllPersonalDetails>>(List.Value);

//    var ListCustomer = List.Value as List<GetAllPersonalDetails>;

//    Assert.Equal(GetCustomer().Count, ListCustomer.Count);



//    //Assert.NotNull(customerResult);
//    //Assert.Equal(GetCustomer().ToString(), customerResult.ToString());
//    //Assert.True(customerList.Equals(customerResult));

//}



//private List<GetAllPersonalCustomerDetails> GetDetails()
//{
//    List<GetAllPersonalCustomerDetails> customerData = new List<GetAllPersonalCustomerDetails>
//     {
//        new GetAllPersonalCustomerDetails
//        {

//            FirstName = "bala",
//            LastName  = "m",
//            Address = "davf",
//            City = "chennai",
//            MobileNumber = "74737",


//        },

//        new GetAllPersonalCustomerDetails
//        {

//            FirstName = "bala",
//            LastName  = "m",
//            Address = "davf",
//            City = "chennai",
//            MobileNumber = "74737",


//        },
//    };
//    return customerData;
//}
//[Fact]
//public async Task Get()
//{
//    //arrange

//    Mock<ICustomerService> mockService = new Mock<ICustomerService>();
//    mockService.Setup(x => x.GetCustomerAsync()).ReturnsAsync(GetDetails());

//    var mockMapper = new Mock<IMapper>();
//    mockMapper.Setup(x => x.Map<GetAllPersonalCustomerDetails>(It.IsAny<GetAllPersonalDetails>()))
//       .Returns((GetAllPersonalDetails details) => GetDetails());
//       //{
//       //    FirstName = "bala",
//       //    LastName = "m",
//       //    Address = "davf",
//       //    City = "chennai",
//       //    MobileNumber = "74737"
//       //});

//    CustomerController controller = new CustomerController(mockService.Object, mockMapper.Object);

//    // Act

//    var result = controller.GetCustomerAsync();

//    // Assert

//    var okResult = await result as OkObjectResult;  
//    Assert.NotNull(okResult);

//    var model = okResult.Value as List<GetAllPersonalDetails>;  
//    Assert.NotNull(model);  
//}
//    }

//}