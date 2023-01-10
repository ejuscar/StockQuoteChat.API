using StockQuoteChat.Application.Entities;

namespace StockQuoteChat.Infrastructure.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task Insert(Message message);
        IQueryable<Message> GetByRoom(Guid roomId);
    }
}
