using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface ITradesRepository
    {
        Task<IEnumerable<Trade>> GetAllTradesAsync();
        Task<Trade> GetTradeByIdAsync(int id);
        Task<Trade> CreateTradeAsync(Trade trade);
        Task<Trade> UpdateTradeAsync(Trade trade);
        Task DeleteTradeAsync(int id);
        Task<bool> TradeExistsAsync(int id);
    }
}
