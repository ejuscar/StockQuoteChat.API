namespace StockQuoteChat.Application.Entities
{
    public class Room
    {
        public Room()
        {
            Id = Guid.NewGuid();
            Messages = new HashSet<Message>();
        }

        public Room(string name, ICollection<Message> messages)
        {
            Id = Guid.NewGuid();
            Name = name;
            Messages = messages;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
