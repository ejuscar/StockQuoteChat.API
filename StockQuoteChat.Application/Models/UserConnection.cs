using StockQuoteChat.Application.Entities;

namespace StockQuoteChat.Application.Models
{
    public class UserConnection
    {
        public UserConnection(User user, Room room)
        {
            User = user;
            Room = room;
        }

        public User User { get; set; }
        public Room Room { get; set; }
    }
}
