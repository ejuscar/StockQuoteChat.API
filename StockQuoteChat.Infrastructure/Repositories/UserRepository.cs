using StockQuoteChat.Application.Entities;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;

namespace StockQuoteChat.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _context;

        public UserRepository(ChatDbContext context)
        {
            _context = context;
        }

        public User? Get(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => 
            string.Equals(u.Email.ToLower(), email.ToLower()) 
            && string.Equals(u.Password, password));
        }
    }
}
