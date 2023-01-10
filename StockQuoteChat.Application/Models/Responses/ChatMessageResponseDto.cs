using StockQuoteChat.Application.Entities;

namespace StockQuoteChat.Application.Models.Responses
{
    public class ChatMessageResponseDto
    {
        public ChatMessageResponseDto(Message message, string userName)
        {
            Timestamp = message.Timestamp;
            Message = message.Body;
            Username = userName;
        }

        public ChatMessageResponseDto(string message, string userName)
        {
            Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            Message = message;
            Username = userName;
        }

        public long Timestamp { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
    }
}
