using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Controllers.Domain;
using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Authorization;
using P7CreateRestApi.Repositories;
using P7CreateRestApi.Model;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsRepository _repository;

        public RatingsController(IRatingsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatings()
        {
            var ratings = await _repository.GetAllRatingsAsync();

            if(ratings == null || !ratings.Any())
            {
                return NotFound();
            }

            return Ok(ratings);
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetRating(int id)
        {
            var rating = await _repository.GetRatingByIdAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, Rating rating)
        {
            var existingRating = await _repository.GetRatingByIdAsync(id);

            if (existingRating == null)
            {
                return NotFound();
            }

            existingRating.MoodysRating = rating.MoodysRating;
            existingRating.SandPRating = rating.SandPRating;
            existingRating.FitchRating = rating.FitchRating;
            existingRating.OrderNumber = rating.OrderNumber;

            await _repository.UpdateRatingAsync(existingRating);

            return NoContent();
        }

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating ratingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rating = new Rating
            {
                MoodysRating = ratingDTO.MoodysRating,
                SandPRating = ratingDTO.SandPRating,
                FitchRating = ratingDTO.FitchRating,
                OrderNumber = ratingDTO.OrderNumber
            };

            var createdRating = await _repository.CreateRatingAsync(rating);
            return CreatedAtAction("GetRating", new { id = createdRating.Id }, createdRating);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _repository.GetRatingByIdAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            await _repository.DeleteRatingAsync(id);

            return NoContent();
        }
    }
}
