using StockQuoteChat.Application.Entities;

namespace StockQuoteChat.Infrastructure.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        IQueryable<Room> GetAll(bool includeMessages);
        Room? Get(Guid id);
    }
}
