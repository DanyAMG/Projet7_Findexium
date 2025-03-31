using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradesRepository _repository;

        public TradesController(ITradesRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Trades
        [HttpGet]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<IEnumerable<Trade>>> GetTrades()
        {
            var trades = await _repository.GetAllTradesAsync();

            if (trades == null || !trades.Any())
            {
                return NotFound();
            }

            return Ok(trades);
        }

        // GET: api/Trades/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Technician, Visitor")]
        public async Task<ActionResult<Trade>> GetTrade(int id)
        {
            var trade = await _repository.GetTradeByIdAsync(id);

            if (trade == null)
            {
                return NotFound();
            }

            return Ok(trade);
        }

        // PUT: api/Trades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<IActionResult> PutTrade(int id, Trade tradeDTO)
        {
            var existingTrade = await _repository.GetTradeByIdAsync(id);

            if (existingTrade == null)
            {
                return NotFound();
            }

            existingTrade.Account = tradeDTO.Account;
            existingTrade.AccountType = tradeDTO.AccountType;
            existingTrade.BuyQuantity = tradeDTO.BuyQuantity;
            existingTrade.SellQuantity = tradeDTO.SellQuantity;
            existingTrade.BuyPrice = tradeDTO.BuyPrice;
            existingTrade.SellPrice = tradeDTO.SellPrice;
            //existingTrade.TradeDate = tradeDTO.TradeDate;
            existingTrade.TradeSecurity = tradeDTO.TradeSecurity;
            existingTrade.TradeStatus = tradeDTO.TradeStatus;
            existingTrade.Trader = tradeDTO.Trader;
            existingTrade.Benchmark = tradeDTO.Benchmark;
            existingTrade.Book = tradeDTO.Book;
            existingTrade.CreationName = tradeDTO.CreationName;
            existingTrade.CreationDate = tradeDTO.CreationDate;
            existingTrade.RevisionName = tradeDTO.RevisionName;
            existingTrade.RevisionDate = tradeDTO.RevisionDate;
            existingTrade.DealName = tradeDTO.DealName;
            existingTrade.DealType = tradeDTO.DealType;
            existingTrade.SourceListId = tradeDTO.SourceListId;
            existingTrade.Side = tradeDTO.Side;

            await _repository.UpdateTradeAsync(existingTrade);

            return NoContent();
        }

        // POST: api/Trades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin, Technician")]
        public async Task<ActionResult<Trade>> PostTrade(Trade tradeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var trade = new Trade
            {
                Account = tradeDTO.Account,
                AccountType = tradeDTO.AccountType,
                BuyQuantity = tradeDTO.BuyQuantity,
                SellQuantity = tradeDTO.SellQuantity,
                BuyPrice = tradeDTO.BuyPrice,
                SellPrice = tradeDTO.SellPrice,
                //TradeDate = tradeDTO.TradeDate,
                TradeSecurity = tradeDTO.TradeSecurity,
                TradeStatus = tradeDTO.TradeStatus,
                Trader = tradeDTO.Trader,
                Benchmark = tradeDTO.Benchmark,
                Book = tradeDTO.Book,
                CreationName = tradeDTO.CreationName,
                CreationDate = tradeDTO.CreationDate,
                RevisionName = tradeDTO.RevisionName,
                RevisionDate = tradeDTO.RevisionDate,
                DealName = tradeDTO.DealName,
                DealType = tradeDTO.DealType,
                SourceListId = tradeDTO.SourceListId,
                Side = tradeDTO.Side
            };

            var createdTrade = await _repository.CreateTradeAsync(trade);
            return CreatedAtAction("GetTrade", new { id = createdTrade.TradeId }, createdTrade);
        }

        // DELETE: api/Trades/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTrade(int id)
        {
            var trade = await _repository.GetTradeByIdAsync(id);
            if (trade == null)
            {
                return NotFound();
            }

            await _repository.DeleteTradeAsync(id);

            return NoContent();
        }
    }
}
