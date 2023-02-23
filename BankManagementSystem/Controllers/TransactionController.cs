
using BankManagementSystem.Domain.Enum;
using BankManagementSystem.Domain.Model;

using BankManagementSystem.Servicess.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using BankManagementSystem.Servicess.Models;
using BankManagementSystem.models;

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _TranService;
        private readonly IMapper _mapper;
        

        public TransactionController(ITransactionService TranService,IMapper mapper)
        {
            _TranService = TranService;
            _mapper = mapper;   
              
            
        }
        [HttpGet("GetAllTransactionDetailsById")]
        public async Task<ActionResult<List<TransactionDetails>>> GetTransactionsByIdAsync()
        {

            try
            {
                var cus = await _TranService.GetTransactionsByIdAsync();

                if (cus.Count == 0)
                    return NoContent();
                return Ok(cus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("GetTransactionDetailsById")]
        public async Task<ActionResult<List<TransactionPersonalDetails>>> GetTransactionsDetailsAsync(long AccountNumber)
        {

            try
            {
                var cus = await _TranService.GetTransactionsDetailsAsync(AccountNumber);
                var transaction = _mapper.Map<List<TransactionPersonalDetails>>(cus);

                if (transaction == null || !transaction.Any())
                    return NoContent();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("GetLastTwoTransactionDetailsById")]
        public async Task<ActionResult<List<TransactionPersonalDetails>>> GetLastTwoTransactionsByIdAsync(long AccountNumber)
        {

            try
            {
                //  var cus = await _TranService.GetLastTwoTransactionsByIdAsync(AccountNumber);
                var cus = await _TranService.GetLastTwoTransactionsByIdAsync(AccountNumber);
                var transaction = _mapper.Map<List<TransactionPersonalDetails>>(cus);

                if (transaction == null || !transaction.Any())
                    return NoContent();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


        [HttpPost("CreateTransaction")]
        public async Task<ActionResult> PostTransactionAsync(PostTransactionDetails request)
        {

            try
            {

                var result = await _TranService.PostTransactionAsync(_mapper.Map<Transaction>(request));

                return Ok("Successful Transaction");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


        }


    }
}


































































//[HttpPost("CreateTransaction")]
//public async Task<ActionResult<TransactionDetails>> PostTransactionAsync(SelectTransactionDetails request, TransType type)
//{

//    try
//    {

//        var result = await _TranService.PostTransactionAsync(request, type);


//        if (result == null)
//            return BadRequest("Not Found AccountNumber");

//        return Ok(result);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }


//}


//[HttpPost("CreateTransaction")]
//public async Task<ActionResult<TransactionDetails>> PostTransactionAsync(SelectTransactionDetails request, TransType type)
//{

//    try
//    {


//        //var result = await _TranService.PostTransactionAsync();

//        var result = await _TranService.PostTransactionAsync(request, type);


//        if (result == null)
//            return BadRequest("400 and Not Found Customer Id");

//        return Ok(result);

//        //  return await _TranService.PostTransactionAsync(request);
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }


//}



//[HttpPost("CreateTransaction")]

//public IActionResult PostTransaction(TransactionDto transactionDTO, TransType type)
//{



//    return Ok(_transactions.DoTransaction(transactionDTO, type));



//}





////private readonly DataContext _dataContext;



////public TransactionController(DataContext datacontext)
////{
////    _dataContext = datacontext;
////}
////[HttpGet("GetAllTransactionDetails")]
////public async Task<ActionResult<List<TransactionDetails>>> Gettransactions(int userId)
////{


////    var user = await _dataContext.Customers.FindAsync(userId);
////    if (user == null)
////        return null;
////    var Transaction = await _dataContext.Transactions.Where(b => b.CustomerId == userId).ToListAsync();
////    return Ok(Transaction);
////}



////[HttpPost("CreateTransaction")]
////public async Task<ActionResult<List<TransactionDetails>>> PostTransactionAsync(SelectTransactionDetails Request)
////{
////    var customer = await _dataContext.Customers.FindAsync(Request.CustomerId);
////    if (customer == null)
////        return null;



////    var newdetail = new TransactionDetails
////    {
////        Amount = Request.Amount,
////        TransactionDate = Request.TransactionDate,
////        TransactionType = Request.TransactionType,
////        CustomerDetails = customer
////    };

////    if (Request.TransactionType == "Withdraw")
////    {
////        customer.CurrentBalance -= Request.Amount;
////    }
////    else
////    {
////        customer.CurrentBalance += Request.Amount;
////    }

////    await _dataContext.Transactions.AddAsync(newdetail);
////    await _dataContext.SaveChangesAsync();
////    return Ok(newdetail);
////}