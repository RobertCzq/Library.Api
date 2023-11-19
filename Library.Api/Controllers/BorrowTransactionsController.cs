using Library.Api.Infrastructure.Models;
using Library.Api.Models;
using Library.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowTransactionsController : ControllerBase
    {
        [HttpGet("{memberId}")]
        public async Task<ActionResult> Get([FromServices] IBorrowTransactionsService borrowTransactionsService, int memberId)
        {
            var transactions = await borrowTransactionsService.GetByMemberId(memberId);
            if (transactions.Any())
            {
                return Ok(transactions);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> AddTransaction([FromServices] IBorrowTransactionsService borrowTransactionsService,
            [FromBody] BorrowTransactionInputModel transaction)
        {
            var added = await borrowTransactionsService.Add(new BorrowTransaction()
            {
                BookId = transaction.BookId,
                MemberId = transaction.MemberId,
                BorrowDate = transaction.BorrowDate
            });
            if (added)
            {
                return Created("", transaction);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromServices] IBorrowTransactionsService borrowTransactionsService,
            int id,
            [FromQuery] DateTime returnDate)
        {
            var updated = await borrowTransactionsService.Update(id, returnDate);

            if (updated)
            {
                return Ok();
            }

            return NoContent();
        }
    }
}
