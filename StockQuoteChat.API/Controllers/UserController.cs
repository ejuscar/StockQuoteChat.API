using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockQuoteChat.Application.Entities;
using StockQuoteChat.Application.Models.Requests;
using StockQuoteChat.Application.Models.Responses;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;

namespace StockQuoteChat.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult<ApiResponseDto<LoginResponseDto>> Authenticate([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _repository.Get(loginRequest.Email, loginRequest.Password);

            if (user == null)
                return NotFound(new ApiResponseDto<LoginResponseDto> (null, false, "Invalid credentials"));

            var token = TokenService.GenerateToken(user);

            var loginResponse = new LoginResponseDto(user, token);

            return Ok(new ApiResponseDto<LoginResponseDto>(loginResponse, true, string.Empty));
        }

    }
}
