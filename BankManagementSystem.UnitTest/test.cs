//using AutoMapper;
//using BankManagementSystem.Controllers;
//using BankManagementSystem.Model;
//using BankManagementSystem.Servicess.InterfaceService;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BankManagementSystem.UnitTest
//{
//    internal class test
//    {
//        private List<CustomerDetails> GetCustomer()
//        {
//            List<CustomerDetails> customerData = new List<CustomerDetails>
//                 {
//                    new CustomerDetails
//                    {
//                        AccountNumber = 139328100,
//                        FirstName = "bala",
//                        LastName  = "m",
//                        Address = "davf",
//                        City = "chennai",
//                        MobileNumber = "74737",
//                        AccountHolder = "BALA M",
//                        IFSCCode = "ioba3728",
//                        BankName = "IOB",
//                        Branch = "chennai",
//                        CurrentBalance = 2000

//                    },
//                };
//            return customerData;
//        }

//        private List<CustomerDetails> GetCustomerEmptyData()
//        {
//            List<CustomerDetails> customerData = new List<CustomerDetails>
//            {

//            };
//            return customerData;
//        }

//        [Fact]
//        public async Task GetCustomers()
//        {

//            //arrange

//            var customerService = new Mock<ICustomerService>();
//            var mapper = new Mock<IMapper>();

//            customerService.Setup(x => x.GetCustomerDataAsync()).ReturnsAsync(GetCustomer());

//            var result = new CustomerController(customerService.Object, mapper.Object);

//            //Act

//            var customerResult = await result.GetCustomerDataAsync();


//            //Assert

//            Assert.IsType<OkObjectResult>(customerResult.Result);

//            var Listed = customerResult.Result as OkObjectResult;

//            Assert.IsType<List<CustomerDetails>>(Listed.Value);

//            var ListCustomer = Listed.Value as List<CustomerDetails>;



//            Assert.Equal(GetCustomer().Count, ListCustomer.Count);



//            //Assert.NotNull(customerResult);
//            //Assert.Equal(GetCustomer().ToString(), customerResult.ToString());
//            //Assert.True(customerList.Equals(customerResult));

//        }


//        [Fact]
//        public async Task GetReturnStatus204()
//        {

//            //arrange

//            var customerService = new Mock<ICustomerService>();
//            var mapper = new Mock<IMapper>();

//            customerService.Setup(x => x.GetCustomerDataAsync()).ReturnsAsync(GetCustomerEmptyData());

//            var result = new CustomerController(customerService.Object, mapper.Object);

//            //Act

//            var customerResult = await result.GetCustomerDataAsync();


//            //Assert

//            Assert.IsType<NoContentResult>(customerResult.Result);

//            var cus = customerResult.Result as NoContentResult;

//            Assert.Equal("204", (cus).StatusCode.ToString());


//        }
//    }
//}





//private CustomerController _customercontroller;
//private GetAllPersonalDetails _getAllPersonalDetails = new GetAllPersonalDetails();
//private GetAllPersonalCustomerDetails _getAllPersonalCustomerDetails = new GetAllPersonalCustomerDetails();

//public void Setup()
//{

//    var mappingConfig = new MapperConfiguration(mc =>
//    {
//        mc.AddProfile(new ApiMapping());
//    });
//    _mapper = mappingConfig.CreateMapper();
//    _customerService = Substitute.For<ICustomerService>();
//    _customercontroller = new CustomerController(_customerService, _mapper);
//}

////private List<GetAllPersonalCustomerDetails> ListCustomerEntitys()
////{
////    List<GetAllPersonalCustomerDetails> _getAllPersonalCustomerDetails = new List<GetAllPersonalCustomerDetails>
////    {
////        new GetAllPersonalCustomerDetails
////        {
////                FirstName = "bala",
////                LastName = "m",
////                Address = "davf",
////                City = "chennai",
////                MobileNumber = "74737",

////        }

////    };

////    return _getAllPersonalCustomerDetails;
////}


//private List<GetAllPersonalDetails> ListCustomerEntity()
//{
//    List<GetAllPersonalDetails> _getAllPersonalDetails = new List<GetAllPersonalDetails>
//    {
//        new GetAllPersonalDetails
//        {
//                FirstName = "bala",
//                LastName = "m",
//                Address = "davf",
//                City = "chennai",
//                MobileNumber = "74737",

//        }

//    };

//    return _getAllPersonalDetails;
//}
//[Fact]
//public void GetCustomersDetails()
//{
//    //Arrange
//    List<GetAllPersonalCustomerDetails> _getAllPersonalCustomerDetails = new List<GetAllPersonalCustomerDetails>
//    {
//        new GetAllPersonalCustomerDetails
//        {
//                FirstName = "bala",
//                LastName = "m",
//                Address = "davf",
//                City = "chennai",
//                MobileNumber = "74737",
//        }
//    };

//    var customer = _customerService.GetCustomerAsync().Returns(_getAllPersonalCustomerDetails);

//    //Act

//    var response = _customercontroller.GetCustomerAsync();

//    //Assert

//    var responseresult = response.Result;
//    Assert.IsType<List<GetAllPersonalDetails>>(_getAllPersonalCustomerDetails);
//    var result = responseresult.Result as OkObjectResult;
//    var actualresult = result.Value as List<GetAllPersonalDetails>;

//    Assert.Equal(_getAllPersonalCustomerDetails.Count, actualresult.Count);



//    //customerService.Setup(x => x.GetCustomerAsync()).Returns(_getAllPersonalCustomerDetails);

//    //var result = new CustomerController(customerService.Object, mapper.Object);

//    ////Act

//    //var customerResult = result.GetCustomerAsync();

//    ////Assert
//    //var responseresult = customerResult.Result;


//    //var results = responseresult.Result as OkObjectResult;
//    //var actualresult = results.Value as List<GetAllPersonalDetails>;
//    //Assert.IsType<List<GetAllPersonalDetails>>(actualresult);

//    //Assert.Equal(_getAllPersonalDetail.Count, actualresult.Count);  

//}