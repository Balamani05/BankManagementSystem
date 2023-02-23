using AutoMapper;
using BankManagementSystem.Dto;
using BankManagementSystem.Model;
using BankManagementSystem.Servicess.InterfaceService;
using BankManagementSystem.Servicess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankManagementSystem.Servicess.Models;
using BankManagementSystem.Servicess.Services;

using BankManagementSystem.models;
using System.Collections.Generic;
//using BankManagementSystem.Domain.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using BankManagementSystem.Domain.Model;

namespace BankManagementSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {


        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;

        }

        [HttpGet("GetAllCustomerPersonalDetails")]

        public async Task<ActionResult<List<GetAllPersonalDetails>>> GetCustomerAsync()
        {
            try
            {
                var result = await _customerService.GetCustomerAsync();
                var res = _mapper.Map<List<GetAllPersonalDetails>>(result);

                if (res.Count == 0)
                {
                    return NoContent();
                }
                return Ok(res);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
        [HttpGet("GetCustomerPersonalDetailsById")]
        public async Task<ActionResult<GetAllPersonalDetails>> GetCustomerDetailsByIdAsync(long AccountNumber)
        {
            try
            {

                //if (AccountNumber < 0)
                //    return Ok($"Account Number {AccountNumber} cannot be less than zero.");

                var customer = await _customerService.GetCustomerDetailsByIdAsync(AccountNumber);
                var customers = _mapper.Map<GetAllPersonalDetails>(customer);

                if (customers == null)
                    return NoContent();
                return Ok(customers);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("GetCustomerBankDetails")]

        public async Task<ActionResult<GetCustomerBankDetails>> GetCustomerBankByIdAsync(long AccountNumber)
        {
            try
            {
                //if (AccountNumber < 0)
                //    return Ok($"Customer Id {AccountNumber} cannot be less than zero.");

                var res = await _customerService.GetCustomerBankByIdAsync(AccountNumber);

                var result = _mapper.Map<GetCustomerBankDetails>(res);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetCurrentBalanceById")]

        public async Task<ActionResult<GetCurrentBalance>> GetCurrentBalanceByIdAsync(long AccountNumber)
        {
            try
            {
                ////if (AccountNumber < 0)
                ////    return Ok($"Customer Id {AccountNumber} cannot be less than zero.");

                var res = await _customerService.GetCurrentBalanceByIdAsync(AccountNumber);

                var result = _mapper.Map<GetCurrentBalance>(res);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("CreateNewCustomerDetails")]
        public async Task<ActionResult> CreateCustomerAsync(PostCustomerDetails customer)
        {
            try
            {
                //Validate Inputs
                if (string.IsNullOrEmpty(customer.FirstName))
                    return Problem(statusCode : 400,detail:"Customer Name is empty!");
                if (string.IsNullOrEmpty(customer.LastName))
                    return Problem(statusCode: 400, detail: "Customer LastName is empty!");
                if (string.IsNullOrEmpty(customer.Address))
                    return Problem(statusCode: 400, detail: "Customer Address is empty!");
                if (string.IsNullOrEmpty(customer.City))
                    return Problem(statusCode: 400, detail: "Customer City is empty!");
                if (string.IsNullOrEmpty(customer.MobileNumber))
                    return Problem(statusCode: 400, detail: "Customer MobileNumber is empty!");
                if (string.IsNullOrEmpty(customer.AccountHolder))
                    return Problem(statusCode: 400, detail: "Customer AccountHolder is empty!");
                if (string.IsNullOrEmpty(customer.IFSCCode))
                    return Problem(statusCode: 400, detail: "Customer IFSCCode is empty!");
                if (string.IsNullOrEmpty(customer.BankName))
                    return Problem(statusCode: 400, detail: "Customer BankName is empty!");
                if (string.IsNullOrEmpty(customer.Branch))
                    return Problem(statusCode: 400, detail: "Customer Branch is empty!");



                var res = await _customerService.CreateCustomerAsync(_mapper.Map<CustomerModel>(customer));

                return Ok("Successfull create a Account");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut("UpdateCustomerMobileNumber")]
        public async Task<ActionResult> UpdateCustomerMobileNumberAsync( PutCustomerMobileNumber customer,long AccountNumber)
        {
            try
            {

                //Validate Inputs
                if (string.IsNullOrEmpty(customer.MobileNumber))
                    return Problem(statusCode : 400, detail : "Customer MobileNumber is empty!");


                var res = await _customerService.UpdateCustomerMobileNumberAsync(_mapper.Map<UpdateMobileNumber>(customer), AccountNumber);
                return Ok("Successfull Updated");


            }

            catch (Exception ex)
            {
                return StatusCode(500, ex);
               
            }
        }






        [HttpDelete("DeleteCustomerDetails")]
        public async Task<ActionResult<CustomerDetails>> DeleteCustomerAsync(long AccountNumber)
        {
            try
            {
              
                var customer = await _customerService.DeleteCustomerAsync(AccountNumber);

                if (customer == null)
                {
                    return Problem(statusCode: 400, detail: $"Account number not found :{AccountNumber}");
                }
                   
                return Ok("Customer Removed");

            }
            catch (Exception ex)
            {
                return Problem(statusCode :500,detail :$"Customer details cannot be deleted");
            }
        }
        

    }
}



































//var res = await _customerService.UpdateCustomerMobileNumberAsync(_mapper.Map<UpdateMobileNumber>(customer),AccountNumber);
//var result = _mapper.Map<PutCustomerMobileNumber>(res);

//var result = _mapper.Map<PostCustomerDetails>(res);
//if (result != null)
//    return BadRequest("400");
//return Ok(result);


//[HttpGet("GetAllCustomerPersonalDetails")]

//public async Task<ActionResult<List<GetAllPersonalDetails>>> GetCustomerAsync()
//{
//    try
//    {
//        var res = await _customerService.GetCustomerAsync();
//        var result = _mapper.Map<List<GetAllPersonalDetails>>(res);

//        if (result == null)
//            return BadRequest("Result is Empty");
//        return Ok(result);
//       // return Ok(res);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}


//[HttpGet("GetAllCustomerName")]

//public async Task<ActionResult<List<GetAllCustomerName>>> GetAllCustomerNameAsync()
//{
//    try
//    {
//        var result = _mapper.Map<List<GetAllCustomerName>>(await _customerService.GetAllCustomerNameAsync());

//        //var res = await _customerService.GetAllCustomerNameAsync();
//        //var result = _mapper.Map<List<GetAllCustomerName>>(res);

//        if (result == null)
//            return BadRequest("Not Store the Value");

//        return Ok(result);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}






//[HttpDelete("DeleteCustomer")]
//public async Task<ActionResult<CustomerDetails>> DeleteCustomerAsync(long AccountNumber)
//{
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");


//        var customer = await _customerService.DeleteCustomerAsync(AccountNumber);

//        if (customer != null)
//            return BadRequest($"Account number not found :{AccountNumber}");
//        return Ok(customer);



//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}








//[HttpPut("UpdateCustomerPersonalDetails")]
//public async Task<ActionResult<bool>> UpdateCustomerPersonalDetailsAsync(PutCustomerDetailsId customer)
//{
//    try
//    {


//        //Validate Inputs
//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");


//        //var res = await _customerService.UpdateCustomerPersonalDetailsAsync(_mapper.Map<PutCustomerDetails>(customer));

//        var res = await _customerService.UpdateCustomerPersonalDetailsAsync(_mapper.Map<CustomerModel>(customer));
//        if (res == false)
//            return BadRequest("400");

//        return true;


//    }

//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}






//[HttpPost("CreateCustomerLogin")]
//public async Task<ActionResult<Boolean>> CreateCustomerDetailsAsync(PostCustomerLogin customer)
//{
//    try
//    {
//        //Validate Inputs
//        if (string.IsNullOrEmpty(customer.FirstName))
//            return Ok("Customer Name is empty!");
//        if (string.IsNullOrEmpty(customer.LastName))
//            return Ok("Customer Email is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");


//        var res = await _customerService.CreateCustomerDetailsAsync(_mapper.Map<CustomerModel>(customer));

//        if (res == false)
//            return BadRequest("400");

//        return Ok(true);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}




//[HttpPost("CreateCustomerAccount")]
//public async Task<ActionResult<Boolean>> CreateCustomerAsync(PostCustomerAccount customer)
//{
//    try
//    {
//        //Validate Inputs
//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            return Ok("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            return Ok("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            return Ok("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            return Ok("Customer Branch is empty!");


//        var res = await _customerService.CreateCustomerAsync(_mapper.Map<CustomerModel>(customer));

//        if (res == false)
//            return BadRequest("400");

//        return Ok(true);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}



//[HttpPost("CreateCustomer")]
//public async Task<ActionResult<CustomerDetails>> CreateCustomerAsync(CustomerDetails customer)
//{
//    try
//    {
//        //Validate Inputs
//        if (string.IsNullOrEmpty(customer.FirstName))
//            return Ok("Customer Name is empty!");
//        if (string.IsNullOrEmpty(customer.LastName))
//            return Ok("Customer Email is empty!");
//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");
//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            return Ok("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            return Ok("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            return Ok("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            return Ok("Customer Branch is empty!");


//        //if (string.IsNullOrEmpty(customer.AccountNumber) || customer.AccountNumber.Length < 11 )
//        //    return Ok("AccountNumber Mubet be atleast 10 number");

//        //var number = Convert.ToInt32(customer.MobileNumber);
//        //if (number <= 10)
//        //    return BadRequest("number is less than 10");


//        var result = await _customerService.CreateCustomerAsync(customer);
//        if (result == null)
//            return BadRequest("400");

//        return Ok("Successfully Create the New Data");
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}



//[HttpPut("UpdateCustomer")]
//public async Task<ActionResult<CustomerDetails>> UpdateCustomerAsync(long AccountNumber, CustomerDetails customer)
//{
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");

//        //Validate Inputs

//        if (string.IsNullOrEmpty(customer.FirstName))
//            return Ok("Customer Name is empty!");
//        if (string.IsNullOrEmpty(customer.LastName))
//            return Ok("Customer Email is empty!");
//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");
//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            return Ok("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            return Ok("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            return Ok("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            return Ok("Customer Branch is empty!");


//        var customers = await _customerService.UpdateCustomerAsync(AccountNumber, customer);


//        if (customer == null)
//            return BadRequest($"Account number not found for this number :{AccountNumber}");

//        return Ok("Successfully Update the Details");
//    }

//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}


//[HttpPut("UpdateCustomerPersonalDetails")]
//public async Task<ActionResult<bool>> UpdateCustomerPersonalDetailsAsync(long AccountNumber, PutCustomerDetailsId customer)
//{
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");

//        //Validate Inputs

//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");
//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            return Ok("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            return Ok("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            return Ok("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            return Ok("Customer Branch is empty!");


//        //var result = _mapper.Map<PutCustomerDetails>(customer);

//        //var res =await _customerService.UpdateCustomerPersonalDetailsAsync(AccountNumber, result);

//        var res = await _customerService.UpdateCustomerPersonalDetailsAsync(_mapper.Map<PutCustomerDetails>(AccountNumber, customer));

//        if (res == false)
//            return BadRequest($"Account number not found :{AccountNumber}");

//        return Ok("True");


//    }

//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}



//[HttpPut("UpdateCustomerPersonalDetails")]
//public async Task<ActionResult<PutCustomerDetailsId>> UpdateCustomerPersonalDetailsAsync(long AccountNumber, PutCustomerDetailsId customer)
//{
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");

//        //Validate Inputs

//        //if (string.IsNullOrEmpty(customer.Address))
//        //    return Ok("Customer Address is empty!");
//        //if (string.IsNullOrEmpty(customer.MobileNumber))
//        //    return Ok("Customer MobileNumber is empty!");

//        var result = _mapper.Map<CustomerDetails>(customer);

//        var customers = await _customerService.UpdateCustomerPersonalDetailsAsync(AccountNumber, result);

//        //var result = _mapper.Map<PutCustomerDetailsId>(customers);

//        //var result = _mapper.Map<PutCustomerDetails>(customers);

//        //var res = _mapper.Map<CustomerDetails>(result);


//        if (result == null)
//            return BadRequest($"Account number not found for this number :{AccountNumber}");

//        return Ok("Successfully Update the Details");
//    }

//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}



//[HttpPost("CreateCustomer")]
//public async Task<ActionResult<CustomerDetails>> CreateAsync(PutCustomerDetails customer)
//{
//    try
//    {
//        //Validate Inputs

//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");



//        //if (string.IsNullOrEmpty(customer.AccountNumber) || customer.AccountNumber.Length < 11 )
//        //    return Ok("AccountNumber Mubet be atleast 10 number");

//        //var number = Convert.ToInt32(customer.MobileNumber);
//        //if (number <= 10)
//        //    return BadRequest("number is less than 10");


//        var result = await _customerService.CreateCustomerAsync(customer);
//        if (result == null)
//            return BadRequest("400");

//        return Ok("Successfully Create the New Data");
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpPost("CreateCustomerDetails")]
//public async Task<ActionResult<CustomerDetails>> CreateCustomerDetailsAsync(CreateCustomer customer)
//{
//    try
//    {
//        //Validate Inputs
//        if (string.IsNullOrEmpty(customer.FirstName))
//            return Ok("Customer FirstName is empty!");
//        if (string.IsNullOrEmpty(customer.LastName))
//            return Ok("Customer LastName is empty!");


//        var result = await _customerService.CreateCustomerDetailsAsync(_mapper.Map<PutCustomerDetails>(customer));

//        //var res = _mapper.Map<CreateCustomer>(result);
//        if (result == null)
//            return BadRequest("400");

//        return Ok("Successfully Create the New Data");
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}




//_mapper.Map(customer, customers);
//if (await _customerService.UpdateCustomerDetailsAsync(AccountNumber, customer))
//{
//    return Ok(_mapper.Map<SelectCustomerDetails>(customers));
//}
//return BadRequest("400");



//var res = await _customerService.GetAllCustomerNameAsync();
//var ress = _mapper.Map<List<GetAllPersonalCustomerDetails>>(res);
//var result = _mapper.Map<List<GetAllCustomerName>>(ress);



//  var result = _mapper.Map<List<PutPersonalCustomerDetails>>(await _customerService.UpdateCustomerAsync(id, customer));

//var number = Convert.ToInt32(customer.MobileNumber);
//if (number <= 10)
//    return BadRequest("number is less than 10");

//[HttpPut("UpdateCustomerBankDetails")]
//public async Task<ActionResult<CustomerDetails>> UpdateCustomerBankDetailsByIdAsync(int id, CustomerDetails customer)
//{
//    try
//    {
//        if (id < 0)
//            return Ok($"Customer Id {id} cannot be less than zero.");

//        //Validate Inputs

//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            throw new Exception("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            throw new Exception("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            throw new Exception("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            throw new Exception("Customer Branch is empty!");

//        var customers = await _customerService.UpdateCustomerBankDetailsByIdAsync(id, customer);
//        if (customers == null)
//            return BadRequest($"Customer not found for id :{id}");
//        return Ok(customers);

//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}




//public CustomerController(ICustomerService customerService, IMapper mapper)
//{
//    _customerService = customerService;
//    _mapper = mapper;

//}

//[HttpGet("GetCustomerById")]
//public CustomerDetails GetCustomerById(int id)
//{
//    try
//    {
//        var customer = _customerService.GetCustomerById(id);

//        return customer;

//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}


////[HttpGet("GetCustomer")]
////public List<CustomerDetails> GetCustomer()
////{
////    return _customerService.GetCustomer();
////}


//[HttpGet("GetCustomer")]

//public ActionResult<List<SelectCustomerDetails>> GetCustomer()
//{
//    var res = _customerService.GetCustomer();
//    var result = _mapper.Map<List<SelectCustomerDetails>>(res);
//    return Ok(result);


//}


//[HttpPost("CreateCustomer")]
//public bool CreateCustomer(CustomerDetails employee)
//{
//    try
//    {
//        return _customerService.CreateCustomer(employee);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpPut("UpdateCustomer")]
//public bool UpdateCustomer(int id, CustomerDetails employee)
//{
//    try
//    {
//        return _customerService.UpdateCustomer(id, employee);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpDelete("DeleteCustomer")]
//public bool DeleteCustomer(int id)
//{
//    try
//    {
//        return _customerService.DeleteCustomer(id);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}















//// private readonly ICustomerRepository _repo;
//private readonly ICustomerService _customerService;


//public CustomerController(ICustomerService customerService)
//{
//    _customerService = customerService;


//}


//// GET: api/CustomerDetails
//[HttpGet("GetAll")]
//public async Task<ActionResult<IEnumerable<CustomerDetails>>> GetCustomerTable()
//{

//    return Ok(await _customerService.GetCustomerTableAsync());


//}



//// GET: api/CustomerDetails/5
//[HttpGet("GetCustomerById")]
//public async Task<ActionResult<CustomerDetails>> GetCustomerDetailsById(int id)
//{
//    try
//    {
//        if (id <= 0)
//            return BadRequest("Invalid Id");

//        var customer = await _customerService.GetCustomerDetailsAsync(id);
//        if (customer == null)
//            return BadRequest($"Customer not found for id:{id}");
//        return Ok(customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}



//// PUT: api/CustomerDetails/5
//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//[HttpPut("updateCustomerDetails")]
//public async Task<IActionResult> UpdateCustomerDetails(int id, CustomerDetails customerDetails)
//{
//    //try
//    //{
//    //    if (id <= 0)
//    //        return BadRequest("Invalid Id");

//    //    var customer = await _repo.UpdateCustomerDetailsAsync(id, customerDetails);
//    //    if (customer == null)
//    //        return BadRequest($"Customer not found for id and customerDetails:{id} {customerDetails}");
//    //    return Ok(customer);
//    //}
//    //catch (Exception ex)
//    //{
//    //    throw new Exception(ex.Message);
//    //}


//    return Ok(await _customerService.UpdateCustomerDetailsAsync(id, customerDetails));
//}

//// POST: api/CustomerDetails
//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//[HttpPost]
//public async Task<ActionResult<CustomerDetails>> CreateCustomerDetails(CustomerDetails customerDetails)
//{


//    return Ok(await _customerService.CreateCustomerDetailsAsync(customerDetails));
//}

//// DELETE: api/CustomerDetails/5
//[HttpDelete("RemoveCustomerById")]
//public async Task<IActionResult> RemoveCustomerDetailsById(int id)
//{
//    try
//    {
//        if (id <= 0)
//            return BadRequest("Invalid Id");

//        var customer = await _customerService.RemoveCustomerDetailsAsync(id);
//        if (customer == null)
//            return BadRequest($"Customer not found for id and customerDetails:{id}");
//        return Ok(customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//    //var customerDetails = await _repo.Customers.FirstOrDefaultAsync(id);
//    //if (customerDetails == null)
//    //{
//    //    return null;
//    //}

//    //_repo.Customers.Remove(customerDetails);
//    //await _repo.SaveChangesAsync();

//    //return NoContent();

//    //return Ok(await _repo.RemoveCustomerDetailsAsync(id));
//}




//#region Private Variables
//private ICustomerService _CustomerService;
//#endregion

//#region Constructors
//public CustomerController(ICustomerService customerService)
//{
//    _CustomerService = customerService;
//}
//#endregion

//#region Public Methods


//[HttpGet]
//public List<CustomerDetails> GetEmployees()
//{
//    return _CustomerService.GetEmployees();
//}


//[HttpGet, Route("{id:int}")]
//public CustomerDetails GetEmployeeById(int id)
//{
//    CustomerDetails customer = new CustomerDetails();
//    try
//    {
//        customer = _CustomerService.GetEmployeeById(id);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//    return customer ?? new CustomerDetails();
//}



////[HttpGet, Route("{name}")]
////public CustomerDetails GetEmployeeByName(string name)
////{
////    var customer = _CustomerService.GetEmployeeByName(name);
////    return customer ?? new CustomerDetails();
////}

//[HttpPost]
//public bool CreateEmployee(CustomerDetails customer)
//{
//    try
//    {
//        return _CustomerService.CreateEmployee(customer);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}

//[HttpPut, Route("{id:int}")]
//public bool UpdateEmployee(int id, CustomerDetails customer)
//{
//    try
//    {
//        return _CustomerService.UpdateEmployee(id, customer);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}

//[HttpDelete, Route("{id:int}")]
//public bool DeleteEmployee(int id)
//{
//    try
//    {
//        return _CustomerService.DeleteEmployee(id);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}
