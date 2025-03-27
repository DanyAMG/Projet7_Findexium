using Dot.Net.WebApi.Controllers.Domain;

namespace P7CreateRestApi.Repositories
{
    public interface IRatingsRepository
    {
        Task<IEnumerable<Rating>> GetAllRatingsAsync();
        Task<Rating> GetRatingByIdAsync(int id);
        Task<Rating> CreateRatingAsync(Rating rating);
        Task<Rating> UpdateRatingAsync(Rating rating);
        Task DeleteRatingAsync(int id);
        Task<bool> RatingExistsAsync(int id);
    }
}
