using Moq;
using Xunit;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Dot.Net.WebApi.Controllers.Domain;
using System.Threading.Tasks;
using P7CreateRestApi.Model;
using System.Collections.Generic;

namespace P7CreateRestApi.Tests
{
    public class RatingsControllerTest
    {
        private readonly Mock<IRatingsRepository> _mockRepo;
        private readonly RatingsController _controller;

        public RatingsControllerTest()
        {
            _mockRepo = new Mock<IRatingsRepository>();
            _controller = new RatingsController(_mockRepo.Object);
        }

        // GET: api/Ratings
        [Fact]
        public async Task GetRatings_ReturnsOkResult_WhenRatingsExist()
        {
            // Arrange
            var ratings = new List<Rating>
            {
                new Rating { Id = 1, MoodysRating = "AAA", SandPRating = "AA", FitchRating = "A", OrderNumber = 1 }
            };
            _mockRepo.Setup(repo => repo.GetAllRatingsAsync()).ReturnsAsync(ratings);

            // Act
            var result = await _controller.GetRatings();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Rating>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Rating>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetRatings_ReturnsNotFound_WhenNoRatingsExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllRatingsAsync()).ReturnsAsync(new List<Rating>());

            // Act
            var result = await _controller.GetRatings();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Rating>>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        // GET: api/Ratings/5
        [Fact]
        public async Task GetRating_ReturnsOkResult_WhenRatingExists()
        {
            // Arrange
            var rating = new Rating { Id = 1, MoodysRating = "AAA", SandPRating = "AA", FitchRating = "A", OrderNumber = 1 };
            _mockRepo.Setup(repo => repo.GetRatingByIdAsync(1)).ReturnsAsync(rating);

            // Act
            var result = await _controller.GetRating(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Rating>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<Rating>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetRating_ReturnsNotFound_WhenRatingDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetRatingByIdAsync(1)).ReturnsAsync((Rating)null);

            // Act
            var result = await _controller.GetRating(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Rating>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        // PUT: api/Ratings/5
        [Fact]
        public async Task PutRating_ReturnsNoContent_WhenRatingIsUpdated()
        {
            // Arrange
            var rating = new Rating { MoodysRating = "AAA", SandPRating = "AA", FitchRating = "A", OrderNumber = 1 };
            var existingRating = new Rating { Id = 1, MoodysRating = "A", SandPRating = "A", FitchRating = "B", OrderNumber = 2 };
            _mockRepo.Setup(repo => repo.GetRatingByIdAsync(It.IsAny<int>())).ReturnsAsync(existingRating);
            _mockRepo.Setup(repo => repo.UpdateRatingAsync(It.IsAny<Rating>())).ReturnsAsync((Rating)null);

            // Act
            var result = await _controller.PutRating(1, rating);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutRating_ReturnsNotFound_WhenRatingDoesNotExist()
        {
            // Arrange
            var rating = new Rating { MoodysRating = "AAA", SandPRating = "AA", FitchRating = "A", OrderNumber = 1 };
            _mockRepo.Setup(repo => repo.GetRatingByIdAsync(1)).ReturnsAsync((Rating)null);

            // Act
            var result = await _controller.PutRating(1, rating);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        // POST: api/Ratings
        [Fact]
        public async Task PostRating_ReturnsCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var ratingDTO = new Rating
            {
                MoodysRating = "AAA",
                SandPRating = "AA",
                FitchRating = "A",
                OrderNumber = 1
            };

            var createdRating = new Rating { Id = 1, MoodysRating = "AAA", SandPRating = "AA", FitchRating = "A", OrderNumber = 1 };
            _mockRepo.Setup(repo => repo.CreateRatingAsync(It.IsAny<Rating>())).ReturnsAsync(createdRating);

            // Act
            var result = await _controller.PostRating(ratingDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Rating>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal("GetRating", createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task PostRating_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var ratingDTO = new Rating();
            _controller.ModelState.AddModelError("MoodysRating", "Required");

            // Act
            var result = await _controller.PostRating(ratingDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Rating>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        // DELETE: api/Ratings/5
        [Fact]
        public async Task DeleteRating_ReturnsNoContent_WhenRatingIsDeleted()
        {
            // Arrange
            var rating = new Rating { Id = 1 };
            _mockRepo.Setup(repo => repo.GetRatingByIdAsync(1)).ReturnsAsync(rating);
            _mockRepo.Setup(repo => repo.DeleteRatingAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteRating(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRating_ReturnsNotFound_WhenRatingDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetRatingByIdAsync(1)).ReturnsAsync((Rating)null);

            // Act
            var result = await _controller.DeleteRating(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
