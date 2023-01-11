using Microsoft.AspNetCore.Mvc;
using Moq;
using StockQuoteChat.API.Controllers;
using StockQuoteChat.Application.Entities;
using StockQuoteChat.Application.Models.Requests;
using StockQuoteChat.Application.Models.Responses;
using StockQuoteChat.Infrastructure.Repositories;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;

namespace StockQuoteChat.Test.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly Mock<IUserRepository> _mockRepository;

        const string WRONG_USER_EMAIL = "invalidemail@email.com";
        const string WRONG_USER_PASSWORD = "wrongpass";

        const string USER_EMAIL = "validuser@email.com";
        const string USER_PASSWORD = "pass";

        public UserControllerTest()
        {
            var user = new User("User", "", USER_EMAIL, USER_PASSWORD, new HashSet<UserRoom>());
            _mockRepository = new Mock<IUserRepository>();
            _mockRepository.Setup(m => m.Get(WRONG_USER_EMAIL, WRONG_USER_PASSWORD)).Returns<User>(null);
            _mockRepository.Setup(m => m.Get(USER_EMAIL, USER_PASSWORD)).Returns(user);

            _controller = new UserController(_mockRepository.Object);
        }

        [TestMethod]
        public void ShouldReturnNotFound_WhenInvalidCredentials()
        {
            var request = new LoginRequestDto {
                Email = WRONG_USER_EMAIL,
                Password = WRONG_USER_PASSWORD
            };

            var response = _controller.Authenticate(request);

            Assert.IsInstanceOfType(response.Result, typeof(NotFoundObjectResult));

            var result = response.Result as NotFoundObjectResult;
            var value = result.Value as ApiResponseDto<LoginResponseDto>;

            Assert.AreEqual(result.StatusCode, 404);
            Assert.IsFalse(value.Success);
            Assert.IsNull(value.Data);
            Assert.AreEqual(value.Error, "Invalid credentials");
        }

        [TestMethod]
        public void ShouldReturnOk_WhenValidCredentials()
        {
            var request = new LoginRequestDto {
                Email = USER_EMAIL,
                Password = USER_PASSWORD
            };

            var response = _controller.Authenticate(request);

            Assert.IsInstanceOfType(response.Result, typeof(OkObjectResult));

            var result = response.Result as OkObjectResult;
            var value = result.Value as ApiResponseDto<LoginResponseDto>;

            Assert.AreEqual(result.StatusCode, 200);
            Assert.IsTrue(value.Success);
            Assert.IsNotNull(value.Data);
            Assert.AreEqual(value.Error, string.Empty);
        }
    }
}
