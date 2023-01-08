namespace StockQuoteChat.API.Models
{
    public class ChatBotRequestDto
    {
        public ChatBotRequestDto(string room, string stockCode)
        {
            Room = room;
            StockCode = stockCode;
        }

        public string Room { get; set; }
        public string StockCode { get; set; }
    }
}
