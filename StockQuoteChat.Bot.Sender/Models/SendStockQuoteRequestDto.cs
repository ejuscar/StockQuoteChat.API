namespace StockQuoteChat.Bot.Sender.Models
{
    public class SendStockQuoteRequestDto
    {
        public SendStockQuoteRequestDto()
        {
            Room = "";
            StockCode = "";
        }

        public string Room { get; set; }
        public string StockCode { get; set; }
    }
}
