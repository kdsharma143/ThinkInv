using BusinessAccess.Interfaces;
using BusinessAccess.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThinkBridgeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService tansactionService;
        public TransactionController(ITransactionService transService)
        {
            tansactionService = transService;
        }

        [HttpPost]
        [Route("Stocktansaction")]
        public async Task<IActionResult> StockTansaction([FromBody] TransactionRequestModel options)
        {
            var cat = await tansactionService.StockTansaction(options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);
        }


        [HttpGet]
        [Route("Gettransactions")]
        public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionsRequestModel options)
        {
            var cat = await tansactionService.GetTransactions(options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);
        }

        [HttpGet]
        [Route("Getstockrepot")]
        public async Task<IActionResult> GetStockRepot([FromQuery] GetStockRepotRequestModel options)
        {
            var cat = await tansactionService.GetStockRepot(options);

            if (cat.Status == 1)
                return Ok(cat);
            else
                return NotFound(cat);
        }

    }
}
