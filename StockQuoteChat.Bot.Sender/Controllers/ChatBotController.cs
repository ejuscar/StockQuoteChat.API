using Microsoft.AspNetCore.Mvc;
using StockQuoteChat.Bot.Sender.Models;
using StockQuoteChat.Bot.Sender.Services;

namespace StockQuoteChat.Bot.Sender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatBotController : ControllerBase
    {
        private readonly ChatBotService _service;

        public ChatBotController()
        {
            _service = new ChatBotService();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> SendStockQuote([FromBody] SendStockQuoteRequestDto request)
        {
            return Ok(await _service.ProcessMessage(request));
        }
    }
}
