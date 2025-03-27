using Dot.Net.WebApi.Domain;
using P7CreateRestApi.Model;

namespace P7CreateRestApi.Repositories
{
    public interface IBidListRepository
    {
        Task<IEnumerable<BidList>> GetAllBidsAsync();
        Task<BidList> GetBidListByIdAsync(int id);
        Task<BidList> CreateBidListAsync(BidList bidList);
        Task<BidList> UpdateBidListAsync(BidList bidList);
        Task DeleteBidListAsync(int id);
        Task<bool> BidListExistsAsync(int id);
    }
}
