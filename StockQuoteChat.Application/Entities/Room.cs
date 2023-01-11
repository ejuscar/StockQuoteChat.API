namespace StockQuoteChat.Application.Entities
{
    public class Room
    {
        public Room()
        {
            Id = Guid.NewGuid();
            UserRooms = new HashSet<UserRoom>();
        }

        public Room(string name, ICollection<UserRoom> userRooms)
        {
            Id = Guid.NewGuid();
            Name = name;
            UserRooms = userRooms;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserRoom> UserRooms { get; set; }
    }
}
