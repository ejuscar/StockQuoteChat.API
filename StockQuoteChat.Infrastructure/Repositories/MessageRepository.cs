using Microsoft.EntityFrameworkCore.Infrastructure;
using StockQuoteChat.Application.Entities;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace StockQuoteChat.Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository, IMessageRepository
    {
        private readonly IUserRoomRepository _userRoomRepository;
        public MessageRepository(ChatDbContext context, IUserRoomRepository userRoomRepository) : base(context)
        {
            _userRoomRepository = userRoomRepository;
        }

        public IQueryable<Message> GetByRoom(Guid roomId)
        {
            return _context.Messages
                .Where(m => m.RoomId == roomId)
                .OrderByDescending(m => m.Timestamp)
                .Take(50)
                .OrderBy(m => m.Timestamp);
        }

        public async Task Insert(Message message)
        {
            try
            {
                var userRoom = _userRoomRepository.GetById(message.UserId, message.RoomId);
                if (userRoom != null)
                {
                    if (userRoom.Messages != null)
                        userRoom.Messages.Add(message);
                    else
                        userRoom.Messages = new List<Message>() { message };

                    _context.Messages.Add(message);
                    _context.Update(userRoom);
                    await _context.SaveChangesAsync();
                }
                else
                    Console.Error.WriteLine($"Could not found userRoom entity. User: {message.UserId}, Room {message.RoomId}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{ex.Message}. User: {message.UserId}, Room {message.RoomId}");
            }
        }
    }
}
