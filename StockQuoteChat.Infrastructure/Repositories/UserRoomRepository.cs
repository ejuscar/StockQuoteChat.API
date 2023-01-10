using StockQuoteChat.Application.Entities;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;
using System.Net.WebSockets;

namespace StockQuoteChat.Infrastructure.Repositories
{
    public class UserRoomRepository : GenericRepository, IUserRoomRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;

        public UserRoomRepository(ChatDbContext context, IUserRepository userRepository, IRoomRepository roomRepository) : base(context)
        {
            _userRepository = userRepository;
            _roomRepository = roomRepository;
        }

        public UserRoom? GetById(Guid userId, Guid roomId)
        {
            return _context.UserRooms.FirstOrDefault(ur => ur.UserId == userId && ur.RoomId == roomId);
        }

        public bool Insert(User user, Room room)
        {
            try
            {
                if (_context.UserRooms.AsQueryable().FirstOrDefault(ur => ur.UserId == user.Id && ur.RoomId == room.Id) != null)
                    return true;

                else
                {
                    var roomEntity = _roomRepository.Get(room.Id);
                    var userEntity = _userRepository.Get(user.Id);

                    if (roomEntity != null && userEntity != null)
                    {
                        roomEntity.UserRooms.Add(new UserRoom
                        {
                            User = userEntity,
                            Room = roomEntity,
                            Messages = new HashSet<Message>()
                        });

                        _context.Update(roomEntity);
                        _context.SaveChanges();

                        return true;
                    }
                }
                
                return false;

            } catch(Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
        }
    }
}
