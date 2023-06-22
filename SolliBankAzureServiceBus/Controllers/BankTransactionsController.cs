using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolliBankAzureServiceBus.Models.DomainModels;
using SolliBankAzureServiceBus.Services.FoundationServices;

namespace SolliBankAzureServiceBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankTransactionsController : ControllerBase
    {
        private readonly IBankTransactionService _bankTransactionService;

        public BankTransactionsController(IBankTransactionService bankTransactionService)
        {
            _bankTransactionService = bankTransactionService;
        }

        [HttpPost]
        public async Task PostBankTransaction(BankTransaction transaction)
        {
            await _bankTransactionService.AddBankTransaction(transaction);
        }
        [HttpGet]
        public IActionResult GetBankTransactions()
        {
            return Ok(_bankTransactionService.GetBankTransactions());
        }
    }
}
