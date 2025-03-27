using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class TradeRepository : ITradesRepository
    {
        private readonly LocalDbContext _context;

        public TradeRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trade>> GetAllTradesAsync()
        {
            return await _context.Trades.ToListAsync();
        }

        public async Task<Trade> GetTradeByIdAsync(int id)
        {
            return await _context.Trades.FindAsync(id);
        }

        public async Task<Trade> CreateTradeAsync(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return trade;
        }

        public async Task<Trade> UpdateTradeAsync(Trade trade)
        {
            _context.Entry(trade).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return trade;
        }

        public async Task DeleteTradeAsync(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade != null)
            {
                _context.Trades.Remove(trade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TradeExistsAsync(int id)
        {
            return await _context.Trades.AnyAsync(e => e.TradeId == id);
        }
    }
}
