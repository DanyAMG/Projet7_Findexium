using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7CreateRestApi.Tests
{
    public class TradesControllerTest
    {
        public class TradesControllerTests
        {
            private readonly Mock<ITradesRepository> _mockRepo;
            private readonly TradesController _controller;

            public TradesControllerTests()
            {
                _mockRepo = new Mock<ITradesRepository>();
                _controller = new TradesController(_mockRepo.Object);
            }

            // GET: api/Trades
            [Fact]
            public async Task GetTrades_ReturnsOkResult_WhenTradesExist()
            {
                // Arrange
                var trades = new List<Trade>
            {
                new Trade { TradeId = 1, Account = "Test Account", BuyQuantity = 100, SellQuantity = 50, BuyPrice = 10, SellPrice = 12, TradeDate = DateTime.Now, TradeSecurity = "Security1", TradeStatus = "Active", Trader = "Trader1" }
            };
                _mockRepo.Setup(repo => repo.GetAllTradesAsync()).ReturnsAsync(trades);

                // Act
                var result = await _controller.GetTrades();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<Trade>>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var returnValue = Assert.IsAssignableFrom<IEnumerable<Trade>>(okResult.Value);
                Assert.Single(returnValue);
            }

            [Fact]
            public async Task GetTrades_ReturnsNotFound_WhenNoTradesExist()
            {
                // Arrange
                _mockRepo.Setup(repo => repo.GetAllTradesAsync()).ReturnsAsync(new List<Trade>());

                // Act
                var result = await _controller.GetTrades();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<Trade>>>(result);
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }

            // GET: api/Trades/5
            [Fact]
            public async Task GetTrade_ReturnsOkResult_WhenTradeExists()
            {
                // Arrange
                var trade = new Trade { TradeId = 1, Account = "Test Account", BuyQuantity = 100, SellQuantity = 50, BuyPrice = 10, SellPrice = 12, TradeDate = DateTime.Now, TradeSecurity = "Security1", TradeStatus = "Active", Trader = "Trader1" };
                _mockRepo.Setup(repo => repo.GetTradeByIdAsync(1)).ReturnsAsync(trade);

                // Act
                var result = await _controller.GetTrade(1);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Trade>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var returnValue = Assert.IsType<Trade>(okResult.Value);
                Assert.Equal(1, returnValue.TradeId);
            }

            [Fact]
            public async Task GetTrade_ReturnsNotFound_WhenTradeDoesNotExist()
            {
                // Arrange
                _mockRepo.Setup(repo => repo.GetTradeByIdAsync(1)).ReturnsAsync((Trade)null);

                // Act
                var result = await _controller.GetTrade(1);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Trade>>(result);
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }

            // PUT: api/Trades/5
            [Fact]
            public async Task PutTrade_ReturnsNoContent_WhenTradeIsUpdated()
            {
                // Arrange
                var tradeDTO = new Trade { Account = "Updated Account", BuyQuantity = 200, SellQuantity = 100, BuyPrice = 15, SellPrice = 18, TradeDate = DateTime.Now, TradeSecurity = "Security2", TradeStatus = "Completed", Trader = "Trader2" };
                var existingTrade = new Trade { TradeId = 1, Account = "Test Account", BuyQuantity = 100, SellQuantity = 50, BuyPrice = 10, SellPrice = 12, TradeDate = DateTime.Now, TradeSecurity = "Security1", TradeStatus = "Active", Trader = "Trader1" };
                _mockRepo.Setup(repo => repo.GetTradeByIdAsync(It.IsAny<int>())).ReturnsAsync(existingTrade);
                _mockRepo.Setup(repo => repo.UpdateTradeAsync(It.IsAny<Trade>())).ReturnsAsync((Trade)null);

                // Act
                var result = await _controller.PutTrade(1, tradeDTO);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async Task PutTrade_ReturnsNotFound_WhenTradeDoesNotExist()
            {
                // Arrange
                var tradeDTO = new Trade { Account = "Updated Account", BuyQuantity = 200, SellQuantity = 100, BuyPrice = 15, SellPrice = 18, TradeDate = DateTime.Now, TradeSecurity = "Security2", TradeStatus = "Completed", Trader = "Trader2" };
                _mockRepo.Setup(repo => repo.GetTradeByIdAsync(1)).ReturnsAsync((Trade)null);

                // Act
                var result = await _controller.PutTrade(1, tradeDTO);

                // Assert
                var notFoundResult = Assert.IsType<NotFoundResult>(result);
            }

            // POST: api/Trades
            [Fact]
            public async Task PostTrade_ReturnsCreatedAtAction_WhenModelIsValid()
            {
                // Arrange
                var tradeDTO = new Trade
                {
                    Account = "Test Account",
                    BuyQuantity = 100,
                    SellQuantity = 50,
                    BuyPrice = 10,
                    SellPrice = 12,
                    TradeDate = DateTime.Now,
                    TradeSecurity = "Security1",
                    TradeStatus = "Active",
                    Trader = "Trader1"
                };

                var createdTrade = new Trade { TradeId = 1, Account = "Test Account", BuyQuantity = 100, SellQuantity = 50, BuyPrice = 10, SellPrice = 12, TradeDate = DateTime.Now, TradeSecurity = "Security1", TradeStatus = "Active", Trader = "Trader1" };
                _mockRepo.Setup(repo => repo.CreateTradeAsync(It.IsAny<Trade>())).ReturnsAsync(createdTrade);

                // Act
                var result = await _controller.PostTrade(tradeDTO);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Trade>>(result);
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
                Assert.Equal("GetTrade", createdAtActionResult.ActionName);
                Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
            }

            [Fact]
            public async Task PostTrade_ReturnsBadRequest_WhenModelIsInvalid()
            {
                // Arrange
                var tradeDTO = new Trade();
                _controller.ModelState.AddModelError("Account", "Required");

                // Act
                var result = await _controller.PostTrade(tradeDTO);

                // Assert
                var actionResult = Assert.IsType<ActionResult<Trade>>(result);
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
                Assert.Equal(400, badRequestResult.StatusCode);
            }

            // DELETE: api/Trades/5
            [Fact]
            public async Task DeleteTrade_ReturnsNoContent_WhenTradeIsDeleted()
            {
                // Arrange
                var trade = new Trade { TradeId = 1 };
                _mockRepo.Setup(repo => repo.GetTradeByIdAsync(1)).ReturnsAsync(trade);
                _mockRepo.Setup(repo => repo.DeleteTradeAsync(1)).Returns(Task.CompletedTask);

                // Act
                var result = await _controller.DeleteTrade(1);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async Task DeleteTrade_ReturnsNotFound_WhenTradeDoesNotExist()
            {
                // Arrange
                _mockRepo.Setup(repo => repo.GetTradeByIdAsync(1)).ReturnsAsync((Trade)null);

                // Act
                var result = await _controller.DeleteTrade(1);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
