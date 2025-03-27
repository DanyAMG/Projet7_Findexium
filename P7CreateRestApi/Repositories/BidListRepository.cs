using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public class BidListRepository : IBidListRepository
    {
        private readonly LocalDbContext _context;
        
        public BidListRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BidList>> GetAllBidsAsync()
        {
            return await _context.Bids.ToListAsync();
        }

        public async Task<BidList> GetBidListByIdAsync(int id)
        {
            return await _context.Bids.FindAsync(id);
        }

        public async Task<BidList> CreateBidListAsync(BidList bidList)
        {
            _context.Bids.Add(bidList);
            await _context.SaveChangesAsync();
            return bidList;
        }

        public async Task<BidList> UpdateBidListAsync(BidList bidList)
        {
            _context.Entry(bidList).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return bidList;
        }

        public async Task DeleteBidListAsync(int id)
        {
            var bidList = await _context.Bids.FindAsync(id);
            if (bidList != null)
            {
                _context.Bids.Remove(bidList);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BidListExistsAsync(int id)
        {
            return await _context.Bids.AnyAsync(e => e.BidListId == id);
        }
    }
}
