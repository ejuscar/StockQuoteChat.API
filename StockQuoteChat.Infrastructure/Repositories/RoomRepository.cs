using StockQuoteChat.Application.Entities;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;

namespace StockQuoteChat.Infrastructure.Repositories
{
    public class RoomRepository : GenericRepository, IRoomRepository
    {
        public RoomRepository(ChatDbContext context) : base(context)
        {
        }

        public Room? Get(Guid id)
        {
            return _context.Rooms.FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<Room> GetAll(bool includeMessages)
        {
            if (!includeMessages)
            {
                return _context.Rooms.Select(r => new Room
                {
                    Id = r.Id,
                    Name = r.Name,
                    UserRooms = new HashSet<UserRoom>()
                });
            }

            return _context.Rooms.AsQueryable();
        }
    }
}
