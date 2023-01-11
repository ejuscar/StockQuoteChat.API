using StockQuoteChat.Application.Entities;

namespace StockQuoteChat.Infrastructure.Repositories.Interfaces
{
    public interface IUserRoomRepository
    {
        bool Insert(User user, Room room);
        UserRoom? GetById(Guid userId, Guid roomId);
    }
}
