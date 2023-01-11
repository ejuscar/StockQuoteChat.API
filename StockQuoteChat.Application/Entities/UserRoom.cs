namespace StockQuoteChat.Application.Entities
{
    public class UserRoom
    {
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
