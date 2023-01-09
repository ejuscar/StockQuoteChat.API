using StockQuoteChat.Application.Entities;

namespace StockQuoteChat.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? Get(string email, string password);
    }
}
