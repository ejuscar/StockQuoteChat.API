using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockQuoteChat.Application.Entities;
using StockQuoteChat.Application.Models.Responses;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;

namespace StockQuoteChat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _repository;

        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [Route("")]
        [HttpGet]
        public ActionResult<ApiResponseDto<IEnumerable<Room>>> GetAllWithoutMessages()
        {
            var rooms = _repository.GetAll(false);

            return Ok(new ApiResponseDto<IEnumerable<Room>>(rooms, true, string.Empty));
        }
    }
}
