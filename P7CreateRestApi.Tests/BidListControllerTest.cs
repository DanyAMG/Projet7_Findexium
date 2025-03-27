using Moq;
using Xunit;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Dot.Net.WebApi.Domain;
using System.Threading.Tasks;
using P7CreateRestApi.Model;

namespace P7CreateRestApi.Tests
{
    public class BidListsControllerTests
    {
        private readonly Mock<IBidListRepository> _mockRepo;
        private readonly BidListsController _controller;

        public BidListsControllerTests()
        {
            _mockRepo = new Mock<IBidListRepository>();
            _controller = new BidListsController(_mockRepo.Object);
        }

        //Get
        [Fact]
        public async Task GetBids_ReturnsOkResult_WhenBidsExist()
        {
            var bids = new List<BidList> { new BidList { BidListId = 1, Account = "Test Account" } };
            _mockRepo.Setup(repo => repo.GetAllBidsAsync()).ReturnsAsync(bids);

            var result = await _controller.GetBids();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<BidList>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<BidList>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetBids_ReturnsNotFound_WhenNoBidsExist()
        {
            _mockRepo.Setup(repo => repo.GetAllBidsAsync()).ReturnsAsync(new List<BidList>());

            var result = await _controller.GetBids();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<BidList>>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetBidList_ReturnsOkResult_WhenBidListExists()
        {
            // Arrange
            var bidList = new BidList { BidListId = 1, Account = "Test Account" };
            _mockRepo.Setup(repo => repo.GetBidListByIdAsync(1)).ReturnsAsync(bidList);

            // Act
            var result = await _controller.GetBidList(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BidList>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<BidList>(okResult.Value);
            Assert.Equal("Test Account", returnValue.Account);
        }

        [Fact]
        public async Task GetBidList_ReturnsNotFound_WhenBidListDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetBidListByIdAsync(1)).ReturnsAsync((BidList)null);

            // Act
            var result = await _controller.GetBidList(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BidList>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostBidList_ReturnsCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var bidListDTO = new BidListDTO
            {
                Account = "Test Account",
                BidType = "Test Type",
                BidQuantity = 10
            };

            var bidList = new BidList { BidListId = 1, Account = "Test Account" };
            _mockRepo.Setup(repo => repo.CreateBidListAsync(It.IsAny<BidList>())).ReturnsAsync(bidList);

            // Act
            var result = await _controller.PostBidList(bidListDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BidList>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal("GetBidList", createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task PostBidList_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var bidListDTO = new BidListDTO();
            _controller.ModelState.AddModelError("Account", "Required");

            // Act
            var result = await _controller.PostBidList(bidListDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<BidList>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task PutBidList_ReturnsNoContent_WhenBidListIsUpdated()
        {
            // Arrange
            var bidListDTO = new BidListDTO
            {
                Account = "Updated Account",
                BidType = "Updated Type"
            };
            var existingBidList = new BidList { BidListId = 1, Account = "Test Account" };
            _mockRepo.Setup(repo => repo.GetBidListByIdAsync(It.IsAny<int>())).ReturnsAsync(existingBidList);
            _mockRepo.Setup(repo => repo.UpdateBidListAsync(It.IsAny<BidList>())).ReturnsAsync((BidList)null);

            // Act
            var result = await _controller.PutBidList(1, bidListDTO);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutBidList_ReturnsNotFound_WhenBidListDoesNotExist()
        {
            // Arrange
            var bidListDTO = new BidListDTO
            {
                Account = "Updated Account",
                BidType = "Updated Type"
            };
            _mockRepo.Setup(repo => repo.GetBidListByIdAsync(1)).ReturnsAsync((BidList)null);

            // Act
            var result = await _controller.PutBidList(1, bidListDTO);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteBidList_ReturnsNoContent_WhenBidListIsDeleted()
        {
            // Arrange
            var bidList = new BidList { BidListId = 1 };
            _mockRepo.Setup(repo => repo.GetBidListByIdAsync(1)).ReturnsAsync(bidList);
            _mockRepo.Setup(repo => repo.DeleteBidListAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteBidList(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBidList_ReturnsNotFound_WhenBidListDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetBidListByIdAsync(1)).ReturnsAsync((BidList)null);

            // Act
            var result = await _controller.DeleteBidList(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}