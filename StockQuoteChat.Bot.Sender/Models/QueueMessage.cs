namespace StockQuoteChat.Bot.Sender.Models
{
    public class QueueMessage
    {
        public QueueMessage(SendStockQuoteRequestDto requestDto, string quoteValue)
        {
            Message = $"{requestDto.StockCode.ToUpper()} quote is ${quoteValue} per share"; ;
            Room = requestDto.Room;
        }

        public string Message { get; set; }
        public string Room { get; set; }
    }
}
