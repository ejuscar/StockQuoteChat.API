namespace StockQuoteChat.Application.Entities
{
    public class Message
    {
        public Message()
        {
            Id = Guid.NewGuid();
            Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        public Message(long timestamp, string body, Guid userId, Guid roomId, User user, Room room)
        {
            Id = Guid.NewGuid();
            Timestamp = timestamp;
            Body = body;
            UserId = userId;
            RoomId = roomId;
            User = user;
            Room = room;
        }

        public Guid Id { get; set; }
        public long Timestamp { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }
        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
    }
}
