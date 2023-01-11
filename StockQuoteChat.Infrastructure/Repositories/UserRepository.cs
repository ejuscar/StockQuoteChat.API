using StockQuoteChat.Application.Entities;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;

namespace StockQuoteChat.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        public UserRepository(ChatDbContext context) : base(context)
        {
        }

        public User? Get(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => 
            string.Equals(u.Email.ToLower(), email.ToLower()) 
            && string.Equals(u.Password, password));
        }

        public User? Get(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
