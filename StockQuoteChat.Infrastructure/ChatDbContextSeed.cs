using Microsoft.EntityFrameworkCore;
using StockQuoteChat.Application.Entities;

namespace StockQuoteChat.Infrastructure
{
    public class ChatDbContextSeed
    {
        private readonly ModelBuilder _builder;
        public ChatDbContextSeed(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            _builder.Entity<User>().HasData(
                new User("ChatUserOne", "", "userone@email.com", "123", new HashSet<UserRoom>()),
                new User("ChatUserTwo", "", "usertwo@email.com", "123", new HashSet<UserRoom>()),
                new User("Bot", "", "mychatbot@chatmail.com", "botexamplepassword", new HashSet<UserRoom>(), true)
                );

            _builder.Entity<Room>().HasData(
                new Room("Room One", new HashSet<UserRoom>()),
                new Room("Room Two", new HashSet<UserRoom>())
                );
        }
    }
}
