using BankManagementSystem.Ado.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using BankManagementSystem.Controllers;
using System.Configuration;
using BankManagementSystem.Model;

using AutoMapper;
using BankManagementSystem.models;
using System.Collections.Generic;
using BankManagementSystem.Ado.Services.Services.InterfaceService;
using BankManagementSystem.Ado.Services.Model;

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {

        private readonly ICustomersService _customersService;

        private readonly IMapper _mapper;


        public CustomerDetailsController(ICustomersService customersService, IMapper mapper)
        {
            _customersService = customersService;
            _mapper = mapper;
        }



        [HttpGet("GetAllCustomerDetails")]
        public List<Customer> CustomerListFromDB()
        {
            return _customersService.CustomerListFromDB();
        }



        [HttpGet("GetCustomerById")]
        public ActionResult<List<GetCustomerDetails>> GetCustomerById(long AccountNumber)
        {
            //CustomerDetails customer = new CustomerDetails();
            try
            {
                if (AccountNumber < 0)
                    return Ok($"Account Number {AccountNumber} cannot be less than zero.");

                var customer = _customersService.GetCustomerById(AccountNumber);

                var res = _mapper.Map<List<GetCustomerDetails>>(customer);

                if (res == null)
                    return BadRequest($"Account Number not found for this Number:{AccountNumber}");
                return Ok(res);

                //var customer = _customersService.GetCustomerById(AccountNumber);
                //return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("GetCustomerPersonalDetailsById")]
        public ActionResult<List<GetAllPersonalDetails>> GetCustomerDetailsById(long AccountNumber)
        {
            //CustomerDetails customer = new CustomerDetails();
            try
            {
                if (AccountNumber < 0)
                    return Ok($"Account Number {AccountNumber} cannot be less than zero.");

                var customer = _customersService.GetCustomerDetailsById(AccountNumber);
                var res = _mapper.Map<List<GetAllPersonalDetails>>(customer);
                if (res == null)
                    return BadRequest($"Account Number not found for this Number:{AccountNumber}");
                return Ok(res);

                //var customer = _customersService.GetCustomerById(AccountNumber);
                //return customer;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }



        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<bool>> CreateCustomerAsync(PostsCustomersDetails customer)
        {
            try
            {
                if (string.IsNullOrEmpty(customer.FirstName))
                    return Ok("Customer Name is empty!");
                if (string.IsNullOrEmpty(customer.LastName))
                    return Ok("Customer Email is empty!");
                if (string.IsNullOrEmpty(customer.Address))
                    return Ok("Customer Address is empty!");
                if (string.IsNullOrEmpty(customer.City))
                    return Ok("Customer City is empty!");
                if (string.IsNullOrEmpty(customer.MobileNumber))
                    return Ok("Customer MobileNumber is empty!");
                if (string.IsNullOrEmpty(customer.AccountHolder))
                    return Ok("Customer AccountHolder is empty!");
                if (string.IsNullOrEmpty(customer.IFSCCode))
                    return Ok("Customer IFSCCode is empty!");
                if (string.IsNullOrEmpty(customer.BankName))
                    return Ok("Customer BankName is empty!");
                if (string.IsNullOrEmpty(customer.Branch))
                    return Ok("Customer Branch is empty!");

                var res = await _customersService.CreateCustomerAsync(_mapper.Map<CustomerAllDetails>(customer));
                if (res == false)
                    return BadRequest("400");
                return true;

                //    return _customersService.CreateCustomerDetails(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("UpdateCustomerId")]
        public async Task<ActionResult<bool>> UpdateCustomer(PutCustomerDetailsId customer)
        {
            try
            {

                if (string.IsNullOrEmpty(customer.Address))
                    return Ok("Customer Address is empty!");
                if (string.IsNullOrEmpty(customer.City))
                    return Ok("Customer City is empty!");
                if (string.IsNullOrEmpty(customer.MobileNumber))
                    return Ok("Customer MobileNumber is empty!");

                var res = await _customersService.UpdateCustomerAsync(_mapper.Map<CustomerAllDetails>(customer));
                if (res == false)
                    return BadRequest("400");
                return true;
                //   return _customersService.UpdateCustomerDetails(AccountNumber, customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete("RemoveCustomerById")]
        public async Task<ActionResult<bool>> DeleteCustomerById(long AccountNumber)
        {
            try
            {
                if (AccountNumber < 0)
                    return Ok($"Account Number {AccountNumber} cannot be less than zero.");


                var customer = await _customersService.DeleteCustomerByIdAsync(AccountNumber);

                if (customer == false)
                    return BadRequest($"Account number not found :{AccountNumber}");
                return true;

                //   return _customersService.DeleteCustomerById(AccountNumber);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}



































//[HttpPost("CreateCustomerDetails")]
//public ActionResult<PostCustomerDetails> CreateCustomerDetails(PostCustomerDetails customer)
//{
//    try
//    {
//        if (string.IsNullOrEmpty(customer.FirstName))
//            return Ok("Customer Name is empty!");
//        if (string.IsNullOrEmpty(customer.LastName))
//            return Ok("Customer Email is empty!");
//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            return Ok("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            return Ok("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            return Ok("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            return Ok("Customer Branch is empty!");

//        var res = _customersService.CreateCustomerDetails(_mapper.Map<CustomerAllDetails>(customer));
//        if (res == null)
//            return BadRequest("400");
//        return Ok("Add New Customer");

//        //    return _customersService.CreateCustomerDetails(customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpPut("UpdateCustomerById")]
//public ActionResult<PutCustomerDetailsId> UpdateCustomerDetails(PutCustomerDetailsId customer)
//{
//    try
//    {

//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");

//        var res = _customersService.UpdateCustomerDetails(_mapper.Map<CustomerAllDetails>(customer));
//        if (res == null)
//            return BadRequest("400");
//        return Ok("Successfully Update data");
//        //   return _customersService.UpdateCustomerDetails(AccountNumber, customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpGet("GetAllCustomerDetails")]
//public List<Customer> GetCustomer()
//{
//    return _customersService.GetCustomer();
//}



//[HttpGet("GetCustomerById")]
//public ActionResult<CustomerAllDetails> GetCustomerById(long AccountNumber)
//{
//    //CustomerDetails customer = new CustomerDetails();
//    try
//    {


//        var customer = _customersService.GetCustomerById(AccountNumber);

//        var res = _mapper.Map<GetCustomerDetails>(customer);

//        if (res == null)
//            return BadRequest($"Account Number not found for this Number:{AccountNumber}");
//        return Ok(res);

//        //var customer = _customersService.GetCustomerById(AccountNumber);
//        //return customer;
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}

//[HttpGet("GetCustomerPersonalDetailsById")]
//public ActionResult<GetAllCustomerPersonalDetails> GetCustomerDetailsById(long AccountNumber)
//{
//    //CustomerDetails customer = new CustomerDetails();
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");

//        var customer = _customersService.GetCustomerDetailsById(AccountNumber);
//        var res = _mapper.Map<GetAllCustomerPersonalDetails>(customer);
//        if (res == null)
//            return BadRequest($"Account Number not found for this Number:{AccountNumber}");
//        return Ok(res);

//        //var customer = _customersService.GetCustomerById(AccountNumber);
//        //return customer;
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}



//[HttpPost("CreateCustomerDetails")]
//public ActionResult<bool> CreateCustomer(PostCustomerDetails customer)
//{
//    try
//    {
//        if (string.IsNullOrEmpty(customer.FirstName))
//            return Ok("Customer Name is empty!");
//        if (string.IsNullOrEmpty(customer.LastName))
//            return Ok("Customer Email is empty!");
//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            return Ok("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            return Ok("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            return Ok("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            return Ok("Customer Branch is empty!");

//        var res = _customersService.CreateCustomer(_mapper.Map<CustomerAllDetails>(customer));
//        if (res == null)
//            return BadRequest("400");
//        return Ok(res);

//        //    return _customersService.CreateCustomerDetails(customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpPut("UpdateCustomerById")]
//public async Task<ActionResult<bool>> UpdateCustomerAsync(PutCustomerDetailsId customer)
//{
//    try
//    {

//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");

//        var res = await _customersService.UpdateCustomerAsync(_mapper.Map<CustomerAllDetails>(customer));
//        if (res == false)
//            return BadRequest("400");
//        return Ok(res);
//        // return _customersService.UpdateCustomer(AccountNumber, customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpDelete("RemoveCustomerById")]
//public ActionResult<bool> DeleteCustomer(long AccountNumber)
//{
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");


//        var customer = _customersService.DeleteCustomer(AccountNumber);

//        if (customer == false)
//            return BadRequest($"Account number not found :{AccountNumber}");
//        return true;

//        //   return _customersService.DeleteCustomerById(AccountNumber);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}



// [HttpGet("GetAllCustomerDetails")]
//public List<Customer> CustomerListFromDB()
//{
//    return _customersService.CustomerListFromDB();
//}



//[HttpGet("GetCustomerById")]
//public ActionResult<List<GetCustomerDetails>> GetCustomerById(long AccountNumber)
//{
//    //CustomerDetails customer = new CustomerDetails();
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");

//        var customer = _customersService.GetCustomerById(AccountNumber);

//        var res = _mapper.Map<List<GetCustomerDetails>>(customer);

//        if (res == null)
//            return BadRequest($"Account Number not found for this Number:{AccountNumber}");
//        return Ok(res);

//        //var customer = _customersService.GetCustomerById(AccountNumber);
//        //return customer;
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}

//[HttpGet("GetCustomerPersonalDetailsById")]
//public ActionResult<List<GetAllCustomerPersonalDetails>> GetCustomerDetailsById(long AccountNumber)
//{
//    //CustomerDetails customer = new CustomerDetails();
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");

//        var customer = _customersService.GetCustomerDetailsById(AccountNumber);
//        var res = _mapper.Map<List<GetAllCustomerPersonalDetails>>(customer);
//        if (res == null)
//            return BadRequest($"Account Number not found for this Number:{AccountNumber}");
//        return Ok(res);

//        //var customer = _customersService.GetCustomerById(AccountNumber);
//        //return customer;
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}



//[HttpPost("CreateCustomerDetails")]
//public ActionResult<PostCustomerDetails> CreateCustomerDetails(PostCustomerDetails customer)
//{
//    try
//    {
//        if (string.IsNullOrEmpty(customer.FirstName))
//            return Ok("Customer Name is empty!");
//        if (string.IsNullOrEmpty(customer.LastName))
//            return Ok("Customer Email is empty!");
//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            return Ok("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            return Ok("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            return Ok("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            return Ok("Customer Branch is empty!");

//        var res = _customersService.CreateCustomerDetails(_mapper.Map<CustomerAllDetails>(customer));
//        if (res == null)
//            return BadRequest("400");
//        return Ok(res);

//        //    return _customersService.CreateCustomerDetails(customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpPut("UpdateCustomerById")]
//public ActionResult<PutCustomerDetailsId> UpdateCustomerDetails(PutCustomerDetailsId customer)
//{
//    try
//    {

//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");

//        var res = _customersService.UpdateCustomerDetails(_mapper.Map<CustomerAllDetails>(customer));
//        if (res == null)
//            return BadRequest("400");
//        return Ok(res);
//        //   return _customersService.UpdateCustomerDetails(AccountNumber, customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpDelete("RemoveCustomerById")]
//public ActionResult<bool> DeleteCustomerById(long AccountNumber)
//{
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");


//        var customer = _customersService.DeleteCustomerById(AccountNumber);

//        if (customer == false)
//            return BadRequest($"Account number not found :{AccountNumber}");
//        return true;

//        //   return _customersService.DeleteCustomerById(AccountNumber);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }



//private readonly ICustomersService _customersService;

//private readonly IMapper _mapper;


//public CustomerDetailsController(ICustomersService customersService, IMapper mapper)
//{
//    _customersService = customersService;
//    _mapper = mapper;
//}

//[HttpGet("GetAllCustomerDetails")]
//public List<Customer> CustomerListFromDB()
//{
//    return _customersService.CustomerListFromDB();
//}



//[HttpGet("GetCustomerById")]
//public ActionResult<List<GetCustomerDetails>> GetCustomerById(long AccountNumber)
//{
//    //CustomerDetails customer = new CustomerDetails();
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");

//        var customer = _customersService.GetCustomerById(AccountNumber);

//        var res = _mapper.Map<List<GetCustomerDetails>>(customer);

//        if (res == null)
//            return BadRequest($"Account Number not found for this Number:{AccountNumber}");
//        return Ok(res);

//        //var customer = _customersService.GetCustomerById(AccountNumber);
//        //return customer;
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}

//[HttpGet("GetCustomerPersonalDetailsById")]
//public ActionResult<List<GetAllCustomerPersonalDetails>> GetCustomerDetailsById(long AccountNumber)
//{
//    //CustomerDetails customer = new CustomerDetails();
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");

//        var customer = _customersService.GetCustomerDetailsById(AccountNumber);
//        var res = _mapper.Map<List<GetAllCustomerPersonalDetails>>(customer);
//        if (res == null)
//            return BadRequest($"Account Number not found for this Number:{AccountNumber}");
//        return Ok(res);

//        //var customer = _customersService.GetCustomerById(AccountNumber);
//        //return customer;
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }

//}



//[HttpPost("CreateCustomerDetails")]
//public ActionResult<Customer> CreateCustomerDetails(PostCustomerDetails customer)
//{
//    try
//    {
//        if (string.IsNullOrEmpty(customer.FirstName))
//            return Ok("Customer Name is empty!");
//        if (string.IsNullOrEmpty(customer.LastName))
//            return Ok("Customer Email is empty!");
//        if (string.IsNullOrEmpty(customer.AccountHolder))
//            return Ok("Customer AccountHolder is empty!");
//        if (string.IsNullOrEmpty(customer.IFSCCode))
//            return Ok("Customer IFSCCode is empty!");
//        if (string.IsNullOrEmpty(customer.BankName))
//            return Ok("Customer BankName is empty!");
//        if (string.IsNullOrEmpty(customer.Branch))
//            return Ok("Customer Branch is empty!");

//        var res = _customersService.CreateCustomerDetails(_mapper.Map<CustomerAllDetails>(customer));
//        if (res == null)
//            return BadRequest("400");
//        return Ok(res);

//        //    return _customersService.CreateCustomerDetails(customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpPut("UpdateCustomerById")]
//public ActionResult<Customer> UpdateCustomerDetails(PutCustomerDetailsId customer)
//{
//    try
//    {

//        if (string.IsNullOrEmpty(customer.Address))
//            return Ok("Customer Address is empty!");
//        if (string.IsNullOrEmpty(customer.City))
//            return Ok("Customer City is empty!");
//        if (string.IsNullOrEmpty(customer.MobileNumber))
//            return Ok("Customer MobileNumber is empty!");

//        var res = _customersService.UpdateCustomerDetails(_mapper.Map<CustomerAllDetails>(customer));
//        if (res == null)
//            return BadRequest("400");
//        return Ok(res);
//        //   return _customersService.UpdateCustomerDetails(AccountNumber, customer);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}

//[HttpDelete("RemoveCustomerById")]
//public ActionResult<bool> DeleteCustomerById(long AccountNumber)
//{
//    try
//    {
//        if (AccountNumber < 0)
//            return Ok($"Account Number {AccountNumber} cannot be less than zero.");


//        var customer = _customersService.DeleteCustomerById(AccountNumber);

//        if (customer == false)
//            return BadRequest($"Account number not found :{AccountNumber}");
//        return true;

//        //   return _customersService.DeleteCustomerById(AccountNumber);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}







































//private readonly ICustomersRepository _customersRepository;


//public CustomerDetailsController(ICustomersRepository customersRepository)
//{
//    _customersRepository = customersRepository;
//}

//[HttpGet("GetAllCustomerDetails")]
//public List<Customer> CustomerListFromDB()
//{
//    return _customersRepository.CustomerListFromDB();
//}



//[HttpGet("GetCustomerById")]
//public List<Customer> GetCustomerById(long AccountNumber)
//{
//    //CustomerDetails customer = new CustomerDetails();
//    try
//    {
//        var customer = _customersRepository.GetCustomerById(AccountNumber);
//        return customer;
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }

//}



//[HttpPost("CreateCustomerDetails")]
//public Customer CreateCustomerDetails(Customer customer)
//{
//    try
//    {
//        return _customersRepository.CreateCustomerDetails(customer);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}

//[HttpPut("UpdateCustomerById")]
//public Customer UpdateCustomerDetails(long AccountNumber, Customer customer)
//{
//    try
//    {
//        return _customersRepository.UpdateCustomerDetails(AccountNumber, customer);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}

//[HttpDelete("RemoveCustomerById")]
//public bool DeleteCustomerById(long AccountNumber)
//{
//    try
//    {
//        return _customersRepository.DeleteCustomerById(AccountNumber);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}
































//[HttpGet]
//public List<CustomerDetails> GetCustomer()
//{
//    return _customersRepository.GetCustomer();
//}



//[HttpGet("GetCustomerById")]
//public CustomerDetails GetCustomerById(long AccountNumber)
//{
//    CustomerDetails customer = new CustomerDetails();
//    try
//    {
//        customer = _customersRepository.GetCustomerById(AccountNumber);
//        return customer;
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }

//}



//[HttpPost("CreateCustomerDetails")]
//public bool CreateCustomer(CustomerDetails customer)
//{
//    try
//    {
//        return _customersRepository.CreateCustomer(customer);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}

//[HttpPut("UpdateCustomerById")]
//public bool UpdateCustomer(long AccountNumber, CustomerDetails customer)
//{
//    try
//    {
//        return _customersRepository.UpdateCustomer(AccountNumber, customer);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}

//[HttpDelete("RemoveCustomerById")]
//public bool DeleteCustomer(long AccountNumber)
//{
//    try
//    {
//        return _customersRepository.DeleteCustomer(AccountNumber);
//    }
//    catch (Exception ex)
//    {
//        throw ex;
//    }
//}

//    private readonly ICustomersRepository _customersRepository;


//    public CustomerDetailsController(ICustomersRepository customersRepository)
//    {
//        _customersRepository = customersRepository;
//    }   

//    [HttpGet]

//    public async Task<ActionResult> GetAllCustomer()
//    {
//        List<Customer> customer = await _customersRepository.GetAllCustomer();
//        return Ok(customer);
//    }
//}
//}




































//private readonly IConfiguration _configuration;

//public CustomerDetailsController(IConfiguration configuration)
//{
//    _configuration = configuration;
//}




//[HttpGet("GetAllCustomerDetails")]
//public List<Customer> CustomerListFromDB()
//{
//    List<Customer> customerList = new List<Customer>();

//    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//    SqlCommand cmd = new SqlCommand("Select * from Customers", con);
//    SqlDataAdapter da = new SqlDataAdapter(cmd);
//    DataTable dt = new DataTable();
//    da.Fill(dt);

//    for (int i = 0; i < dt.Rows.Count; i++)
//    {
//        Customer obj = new Customer();
//        obj.AccountNumber = long.Parse(dt.Rows[i]["AccountNumber"].ToString());
//        obj.FirstName = dt.Rows[i]["FirstName"].ToString();
//        obj.LastName = dt.Rows[i]["LastName"].ToString();
//        obj.Address = dt.Rows[i]["Address"].ToString();
//        obj.City = dt.Rows[i]["City"].ToString();
//        obj.MobileNumber = dt.Rows[i]["MobileNumber"].ToString();
//        obj.CreatedDate = (DateTime?)dt.Rows[i]["CreatedDate"];
//        obj.AccountHolder = dt.Rows[i]["AccountHolder"].ToString();
//        obj.IFSCCode = dt.Rows[i]["IFSCCODE"].ToString();
//        obj.BankName = dt.Rows[i]["BankName"].ToString();
//        obj.Branch = dt.Rows[i]["Branch"].ToString();
//        obj.CurrentBalance = decimal.Parse(dt.Rows[i]["CurrentBalance"].ToString());

//        customerList.Add(obj);

//    }

//    return customerList;
//}

//[HttpGet("GetCustomerById")]

//public List<Customer> GetCustomerById(long AccountNumber)
//{
//    return CustomerListFromDB().Where(e => e.AccountNumber == AccountNumber).ToList();
//}


//[HttpPost("CreateCustomer")]

//public Customer CreateCustomerDetails(Customer customer)
//{
//    var Date = ((DateTime)customer.CreatedDate).ToString("yyyy-MM-dd hh:mm:ss");
//    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//    SqlCommand cmd = new SqlCommand("Insert into Customers values('" + customer.FirstName + "' ,'" + customer.LastName + "' ,'" + customer.Address +
//        "','" + customer.City + "','" + customer.MobileNumber + "','" + Date + "','" + customer.AccountHolder + "','" + customer.IFSCCode +
//        "','" + customer.BankName + "','" + customer.Branch + "','" + customer.CurrentBalance.ToString() + "')", con);


//    con.Open();
//    cmd.ExecuteNonQuery();
//    con.Close();


//    return customer;
//}



//[HttpPut("UpdateCustomerDetails")]

//public Customer UpdateCustomerDetails(long AccountNumber, Customer customer)
//{
//    var Date = ((DateTime)customer.CreatedDate).ToString("yyyy-MM-dd hh:mm:ss");
//    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//    SqlCommand cmd = new SqlCommand("Update Customers set FirstName ='" + customer.FirstName + "' ,LastName ='" + customer.LastName + "' ,Address = '" + customer.Address +
//        "',City = '" + customer.City + "',MobileNumber = '" + customer.MobileNumber + "',CreatedDate = '" + Date + "',AccountHolder = '" + customer.AccountHolder + "',IFSCCODE = '" + customer.IFSCCode +
//        "',BankName = '" + customer.BankName + "',Branch = '" + customer.Branch + "',CurrentBalance = '" + customer.CurrentBalance + "' where AccountNumber = '" + AccountNumber + "'", con);


//    con.Open();
//    cmd.ExecuteNonQuery();
//    con.Close();

//    return customer;

//}

//[HttpDelete("RemoveCustomerById")]

//public bool DeleteCustomerById(long AccountNumber)
//{

//    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//    SqlCommand cmd = new SqlCommand("Delete from Customers where AccountNumber ='" + AccountNumber + "'", con);

//    con.Open();
//    cmd.ExecuteNonQuery();
//    con.Close();


//    return true;
//}









//private readonly IConfiguration _configuration;

//public CustomerDetailsController(IConfiguration configuration)
//{
//    _configuration = configuration;
//}




//[HttpGet("GetAllCustomerDetails")]
//public List<Customer> CustomerListFromDB()
//{
//    List<Customer> customerList = new List<Customer>();

//    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//    SqlCommand cmd = new SqlCommand("Select * from Customers", con);
//    SqlDataAdapter da = new SqlDataAdapter(cmd);
//    DataTable dt = new DataTable();
//    da.Fill(dt);

//    for (int i = 0; i < dt.Rows.Count; i++)
//    {
//        Customer obj = new Customer();
//        obj.AccountNumber = long.Parse(dt.Rows[i]["AccountNumber"].ToString());
//        obj.FirstName = dt.Rows[i]["FirstName"].ToString();
//        obj.LastName = dt.Rows[i]["LastName"].ToString();
//        obj.Address = dt.Rows[i]["Address"].ToString();
//        obj.City = dt.Rows[i]["City"].ToString();
//        obj.MobileNumber = dt.Rows[i]["MobileNumber"].ToString();
//        obj.CreatedDate = (DateTime?)dt.Rows[i]["CreatedDate"];
//        obj.AccountHolder = dt.Rows[i]["AccountHolder"].ToString();
//        obj.IFSCCode = dt.Rows[i]["IFSCCODE"].ToString();
//        obj.BankName = dt.Rows[i]["BankName"].ToString();
//        obj.Branch = dt.Rows[i]["Branch"].ToString();
//        obj.CurrentBalance = decimal.Parse(dt.Rows[i]["CurrentBalance"].ToString());

//        customerList.Add(obj);

//    }

//    return customerList;
//}

//[HttpGet("GetCustomerById")]

//public List<Customer> GetCustomerById(long AccountNumber)
//{
//    return CustomerListFromDB().Where(e => e.AccountNumber == AccountNumber).ToList();
//}

//[HttpPost("CreateCustomer")]

//public Customer CreateCustomerDetails(Customer customer)
//{
//    var Date = ((DateTime)customer.CreatedDate).ToString("yyyy-MM-dd hh:mm:ss");
//    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//    SqlCommand cmd = new SqlCommand("Insert into Customers values('" + customer.FirstName + "' ,'" + customer.LastName + "' ,'" + customer.Address +
//        "','" + customer.City + "','" + customer.MobileNumber + "','" + Date + "','" + customer.AccountHolder + "','" + customer.IFSCCode +
//        "','" + customer.BankName + "','" + customer.Branch + "','" + customer.CurrentBalance.ToString() + "')", con);


//    con.Open();
//    cmd.ExecuteNonQuery();
//    con.Close();


//    return customer;
//}



//[HttpPut("UpdateCustomerDetails")]

//public Customer UpdateCustomerDetails(long AccountNumber, Customer customer)
//{
//    var Date = ((DateTime)customer.CreatedDate).ToString("yyyy-MM-dd hh:mm:ss");
//    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//    SqlCommand cmd = new SqlCommand("Update Customers set FirstName ='" + customer.FirstName + "' ,LastName ='" + customer.LastName + "' ,Address = '" + customer.Address +
//        "',City = '" + customer.City + "',MobileNumber = '" + customer.MobileNumber + "',CreatedDate = '" + Date + "',AccountHolder = '" + customer.AccountHolder + "',IFSCCODE = '" + customer.IFSCCode +
//        "',BankName = '" + customer.BankName + "',Branch = '" + customer.Branch + "',CurrentBalance = '" + customer.CurrentBalance + "' where AccountNumber = '" + AccountNumber + "'", con);


//    con.Open();
//    cmd.ExecuteNonQuery();
//    con.Close();

//    return customer;

//}

//[HttpDelete("RemoveCustomerById")]
//public bool DeleteCustomerById(long AccountNumber)
//{

//    SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
//    SqlCommand cmd = new SqlCommand("Delete from Customers where AccountNumber ='" + AccountNumber + "'", con);

//    con.Open();
//    cmd.ExecuteNonQuery();
//    con.Close();


//    return true;
//}