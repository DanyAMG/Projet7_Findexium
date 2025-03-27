using Moq;
using Xunit;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Dot.Net.WebApi.Domain;
using System.Threading.Tasks;
using P7CreateRestApi.Model;
using System.Collections.Generic;

namespace P7CreateRestApi.Tests
{
    public class CurvePointsControllerTests
    {
        private readonly Mock<ICurvePointsRepository> _mockRepo;
        private readonly CurvePointsController _controller;

        public CurvePointsControllerTests()
        {
            _mockRepo = new Mock<ICurvePointsRepository>();
            _controller = new CurvePointsController(_mockRepo.Object);
        }

        // GET: api/CurvePoints
        [Fact]
        public async Task GetCurvePoints_ReturnsOkResult_WhenCurvePointsExist()
        {
            // Arrange
            var curvePoints = new List<CurvePoint>
            {
                new CurvePoint { Id = 1, CurveId = 1, AsOfDate = DateTime.Now, CurvePointValue = 100 }
            };
            _mockRepo.Setup(repo => repo.GetAllCurvePointAsync()).ReturnsAsync(curvePoints);

            // Act
            var result = await _controller.GetCurvePoints();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CurvePoint>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<CurvePoint>>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetCurvePoints_ReturnsNotFound_WhenNoCurvePointsExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllCurvePointAsync()).ReturnsAsync(new List<CurvePoint>());

            // Act
            var result = await _controller.GetCurvePoints();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<CurvePoint>>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        // GET: api/CurvePoints/5
        [Fact]
        public async Task GetCurvePoint_ReturnsOkResult_WhenCurvePointExists()
        {
            // Arrange
            var curvePoint = new CurvePoint { Id = 1, CurveId = 1, AsOfDate = DateTime.Now, CurvePointValue = 100 };
            _mockRepo.Setup(repo => repo.GetCurvePointByIdAsync(1)).ReturnsAsync(curvePoint);

            // Act
            var result = await _controller.GetCurvePoint(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CurvePoint>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<CurvePoint>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetCurvePoint_ReturnsNotFound_WhenCurvePointDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCurvePointByIdAsync(1)).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await _controller.GetCurvePoint(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CurvePoint>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        // PUT: api/CurvePoints/5
        [Fact]
        public async Task PutCurvePoint_ReturnsNoContent_WhenCurvePointIsUpdated()
        {
            // Arrange
            var curvePointDTO = new CurvePointDTO
            {
                CurveId = 1,
                AsOfDate = DateTime.Now,
                CurvePointValue = 150
            };
            var existingCurvePoint = new CurvePoint { Id = 1, CurveId = 1, AsOfDate = DateTime.Now, CurvePointValue = 100 };
            _mockRepo.Setup(repo => repo.GetCurvePointByIdAsync(It.IsAny<int>())).ReturnsAsync(existingCurvePoint);
            _mockRepo.Setup(repo => repo.UpdateCurvePointAsync(It.IsAny<CurvePoint>())).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await _controller.PutCurvePoint(1, curvePointDTO);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutCurvePoint_ReturnsNotFound_WhenCurvePointDoesNotExist()
        {
            // Arrange
            var curvePointDTO = new CurvePointDTO
            {
                CurveId = 1,
                AsOfDate = DateTime.Now,
                CurvePointValue = 150
            };
            _mockRepo.Setup(repo => repo.GetCurvePointByIdAsync(1)).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await _controller.PutCurvePoint(1, curvePointDTO);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }

        // POST: api/CurvePoints
        [Fact]
        public async Task PostCurvePoint_ReturnsCreatedAtAction_WhenModelIsValid()
        {
            // Arrange
            var curvePointDTO = new CurvePointDTO
            {
                CurveId = 1,
                AsOfDate = DateTime.Now,
                CurvePointValue = 200
            };
            var curvePoint = new CurvePoint { Id = 1, CurveId = 1, AsOfDate = DateTime.Now, CurvePointValue = 200 };
            _mockRepo.Setup(repo => repo.CreateCurvePointAsync(It.IsAny<CurvePoint>())).ReturnsAsync(curvePoint);

            // Act
            var result = await _controller.PostCurvePoint(curvePointDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CurvePoint>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal("GetCurvePoint", createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task PostCurvePoint_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var curvePointDTO = new CurvePointDTO();
            _controller.ModelState.AddModelError("CurveId", "Required");

            // Act
            var result = await _controller.PostCurvePoint(curvePointDTO);

            // Assert
            var actionResult = Assert.IsType<ActionResult<CurvePoint>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        // DELETE: api/CurvePoints/5
        [Fact]
        public async Task DeleteCurvePoint_ReturnsNoContent_WhenCurvePointIsDeleted()
        {
            // Arrange
            var curvePoint = new CurvePoint { Id = 1 };
            _mockRepo.Setup(repo => repo.GetCurvePointByIdAsync(1)).ReturnsAsync(curvePoint);
            _mockRepo.Setup(repo => repo.DeleteCurvePointAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCurvePoint(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCurvePoint_ReturnsNotFound_WhenCurvePointDoesNotExist()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetCurvePointByIdAsync(1)).ReturnsAsync((CurvePoint)null);

            // Act
            var result = await _controller.DeleteCurvePoint(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
