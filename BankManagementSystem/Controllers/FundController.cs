using AutoMapper;
using BankManagementSystem.Domain.Model;
using BankManagementSystem.models;
using BankManagementSystem.Servicess.InterfaceService;
using BankManagementSystem.Servicess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundController : ControllerBase
    {
        private readonly IMoneyTransactionService _MoneyTranService;
        private readonly IMapper _mapper;


        public FundController(IMoneyTransactionService MoneyTranService, IMapper mapper)
        {
            _MoneyTranService = MoneyTranService;
            _mapper = mapper;


        }

        [HttpGet("GetTransactionById")]

        public async Task<ActionResult<List<GetFundTransactionById>>> GetMoneyTransactionsById(long AccountNumber)
        {
            try
            {
                var details = await _MoneyTranService.GetMoneyTransactionsById(AccountNumber);
                var result = _mapper.Map<List<GetFundTransactionById>>(details);

                if (result == null || !result.Any())
                    return NoContent();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("CreateTransaction")]
        public async Task<ActionResult> PostTransactionAsync(PostFundTransaction request)
        {

            try
            {

                var result = await _MoneyTranService.PostTransactionAsync(_mapper.Map<MoneyTransaction>(request));

                return Ok("Successful Transaction");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }


        }
    }
}
