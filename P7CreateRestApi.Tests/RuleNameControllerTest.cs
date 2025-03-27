using Dot.Net.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Tests
{
    public class RuleNameControllerTest
    {
        public class RuleNamesControllerTests
        {
            private readonly Mock<IRuleNamesRepository> _mockRepo;
            private readonly RuleNamesController _controller;

            public RuleNamesControllerTests()
            {
                _mockRepo = new Mock<IRuleNamesRepository>();
                _controller = new RuleNamesController(_mockRepo.Object);
            }

            // GET: api/RuleNames
            [Fact]
            public async Task GetRules_ReturnsOkResult_WhenRulesExist()
            {
                // Arrange
                var ruleNames = new List<RuleName>
            {
                new RuleName { Id = 1, Username = "admin", Description = "Test Rule", JSon = "{}", Template = "Template1", SqlStr = "SELECT * FROM Rules", SqlPart = "WHERE" }
            };
                _mockRepo.Setup(repo => repo.GetAllRuleNamesAsync()).ReturnsAsync(ruleNames);

                // Act
                var result = await _controller.GetRules();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<RuleName>>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var returnValue = Assert.IsAssignableFrom<IEnumerable<RuleName>>(okResult.Value);
                Assert.Single(returnValue);
            }

            [Fact]
            public async Task GetRules_ReturnsNotFound_WhenNoRulesExist()
            {
                // Arrange
                _mockRepo.Setup(repo => repo.GetAllRuleNamesAsync()).ReturnsAsync(new List<RuleName>());

                // Act
                var result = await _controller.GetRules();

                // Assert
                var actionResult = Assert.IsType<ActionResult<IEnumerable<RuleName>>>(result);
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }

            // GET: api/RuleNames/5
            [Fact]
            public async Task GetRuleName_ReturnsOkResult_WhenRuleExists()
            {
                // Arrange
                var ruleName = new RuleName { Id = 1, Username = "admin", Description = "Test Rule", JSon = "{}", Template = "Template1", SqlStr = "SELECT * FROM Rules", SqlPart = "WHERE" };
                _mockRepo.Setup(repo => repo.GetRuleNameByIdAsync(1)).ReturnsAsync(ruleName);

                // Act
                var result = await _controller.GetRuleName(1);

                // Assert
                var actionResult = Assert.IsType<ActionResult<RuleName>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var returnValue = Assert.IsType<RuleName>(okResult.Value);
                Assert.Equal(1, returnValue.Id);
            }

            [Fact]
            public async Task GetRuleName_ReturnsNotFound_WhenRuleDoesNotExist()
            {
                // Arrange
                _mockRepo.Setup(repo => repo.GetRuleNameByIdAsync(1)).ReturnsAsync((RuleName)null);

                // Act
                var result = await _controller.GetRuleName(1);

                // Assert
                var actionResult = Assert.IsType<ActionResult<RuleName>>(result);
                Assert.IsType<NotFoundResult>(actionResult.Result);
            }

            // PUT: api/RuleNames/5
            [Fact]
            public async Task PutRuleName_ReturnsNoContent_WhenRuleIsUpdated()
            {
                // Arrange
                var ruleNameDTO = new RuleName { Username = "admin", Description = "Updated Rule", JSon = "{}", Template = "Template2", SqlStr = "SELECT * FROM Rules", SqlPart = "WHERE" };
                var existingRule = new RuleName { Id = 1, Username = "admin", Description = "Test Rule", JSon = "{}", Template = "Template1", SqlStr = "SELECT * FROM Rules", SqlPart = "WHERE" };
                _mockRepo.Setup(repo => repo.GetRuleNameByIdAsync(It.IsAny<int>())).ReturnsAsync(existingRule);
                _mockRepo.Setup(repo => repo.UpdateRuleNameAsync(It.IsAny<RuleName>())).ReturnsAsync((RuleName)null);

                // Act
                var result = await _controller.PutRuleName(1, ruleNameDTO);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async Task PutRuleName_ReturnsNotFound_WhenRuleDoesNotExist()
            {
                // Arrange
                var ruleNameDTO = new RuleName { Username = "admin", Description = "Updated Rule", JSon = "{}", Template = "Template2", SqlStr = "SELECT * FROM Rules", SqlPart = "WHERE" };
                _mockRepo.Setup(repo => repo.GetRuleNameByIdAsync(1)).ReturnsAsync((RuleName)null);

                // Act
                var result = await _controller.PutRuleName(1, ruleNameDTO);

                // Assert
                var notFoundResult = Assert.IsType<NotFoundResult>(result);
            }

            // POST: api/RuleNames
            [Fact]
            public async Task PostRuleName_ReturnsCreatedAtAction_WhenModelIsValid()
            {
                // Arrange
                var ruleNameDTO = new RuleName
                {
                    Username = "admin",
                    Description = "New Rule",
                    JSon = "{}",
                    Template = "Template3",
                    SqlStr = "SELECT * FROM Rules",
                    SqlPart = "WHERE"
                };

                var createdRuleName = new RuleName { Id = 1, Username = "admin", Description = "New Rule", JSon = "{}", Template = "Template3", SqlStr = "SELECT * FROM Rules", SqlPart = "WHERE" };
                _mockRepo.Setup(repo => repo.CreateRuleNameAsync(It.IsAny<RuleName>())).ReturnsAsync(createdRuleName);

                // Act
                var result = await _controller.PostRuleName(ruleNameDTO);

                // Assert
                var actionResult = Assert.IsType<ActionResult<RuleName>>(result);
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
                Assert.Equal("GetRuleName", createdAtActionResult.ActionName);
                Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
            }

            [Fact]
            public async Task PostRuleName_ReturnsBadRequest_WhenModelIsInvalid()
            {
                // Arrange
                var ruleNameDTO = new RuleName();
                _controller.ModelState.AddModelError("Username", "Required");

                // Act
                var result = await _controller.PostRuleName(ruleNameDTO);

                // Assert
                var actionResult = Assert.IsType<ActionResult<RuleName>>(result);
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
                Assert.Equal(400, badRequestResult.StatusCode);
            }

            // DELETE: api/RuleNames/5
            [Fact]
            public async Task DeleteRuleName_ReturnsNoContent_WhenRuleIsDeleted()
            {
                // Arrange
                var ruleName = new RuleName { Id = 1 };
                _mockRepo.Setup(repo => repo.GetRuleNameByIdAsync(1)).ReturnsAsync(ruleName);
                _mockRepo.Setup(repo => repo.DeleteRuleNameAsync(1)).Returns(Task.CompletedTask);

                // Act
                var result = await _controller.DeleteRuleName(1);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

            [Fact]
            public async Task DeleteRuleName_ReturnsNotFound_WhenRuleDoesNotExist()
            {
                // Arrange
                _mockRepo.Setup(repo => repo.GetRuleNameByIdAsync(1)).ReturnsAsync((RuleName)null);

                // Act
                var result = await _controller.DeleteRuleName(1);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
