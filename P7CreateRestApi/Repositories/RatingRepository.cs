using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Controllers.Domain;

namespace P7CreateRestApi.Repositories
{
    public class RatingRepository : IRatingsRepository
    {
        private readonly LocalDbContext _context;

        public RatingRepository(LocalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rating>> GetAllRatingsAsync()
        {
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating> GetRatingByIdAsync(int id)
        {
            return await _context.Ratings.FindAsync(id);
        }

        public async Task<Rating> CreateRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> UpdateRatingAsync(Rating rating)
        {
            _context.Entry(rating).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task DeleteRatingAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if(rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RatingExistsAsync(int id)
        {
            return await _context.Ratings.AnyAsync(e => e.Id == id);
        }
    }
}
