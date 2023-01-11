using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StockQuoteChat.Application.Entities;
using StockQuoteChat.Application.Models;
using StockQuoteChat.Application.Models.Responses;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;

namespace StockQuoteChat.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botGroupName;
        private readonly string _command;
        private readonly IDictionary<string, UserConnection> _connections;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRoomRepository _userRoomRepository;

        public ChatHub(IDictionary<string, UserConnection> connections, IMessageRepository messageRepository, IUserRoomRepository userRoomRepository)
        {
            _botGroupName = "BotGroup";
            _connections = connections;
            _command = "/stock=";
            _messageRepository = messageRepository;
            _userRoomRepository = userRoomRepository;
        }

        [Authorize]
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);

                Clients
                    .Group(_botGroupName)
                    .SendAsync("UserLeft", userConnection);

                Groups.RemoveFromGroupAsync(Context.ConnectionId, userConnection.Room.Id.ToString());
            }
            return base.OnDisconnectedAsync(exception);
        }

        [Authorize]
        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                Message messageEntity = new Message(message, userConnection);
                
                // Send Command to bot and don't save on database
                if (message.ToLower().Contains(_command))
                    await Clients
                        .Group(_botGroupName)
                        .SendAsync("ReceiveCommand", message, userConnection);

                else
                    await _messageRepository.Insert(messageEntity);

                await Clients.Group(userConnection.Room.Id.ToString())
                    .SendAsync("ReceiveMessage", new ChatMessageResponseDto(messageEntity, userConnection.User.GetUserName()));
            }
        }

        [Authorize(Roles = "Bot")]
        public async Task SendBotMessage(string message, UserConnection botConnection, bool saveMessage)
        {
            Message messageEntity = new Message(message, botConnection);

            if (saveMessage)
                await _messageRepository.Insert(messageEntity);

            await Clients.Group(botConnection.Room.Id.ToString())
                .SendAsync("ReceiveMessage", new ChatMessageResponseDto(messageEntity, botConnection.User.GetUserName()));

        }

        [Authorize]
        public async Task JoinRoom(UserConnection userConnection)
        {
            if (await AddUserToRoom(userConnection))
            {
                _connections[Context.ConnectionId] = userConnection;

                var messagesLoaded = _messageRepository.GetByRoom(userConnection.Room.Id)
                    .Include(m => m.UserRoom.User)
                    .Select(m => new ChatMessageResponseDto(m, m.UserRoom.User.GetUserName()))
                    .ToList();

                await Clients
                    .Clients(Context.ConnectionId)
                    .SendAsync("LoadMessages", messagesLoaded);

                await Clients
                    .Group(_botGroupName)
                    .SendAsync("UserJoined", userConnection);
            }

            else
            {
                throw new Exception("Error while trying to join in room");
            }
        }

        [Authorize]
        public async Task<bool> AddUserToRoom(UserConnection userConnection)
        {
            bool success = _userRoomRepository.Insert(userConnection.User, userConnection.Room);
            if (success)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room.Id.ToString());
                return true;
            }

            return false;
        }

        [Authorize(Roles = "Bot")]
        public async void AddBotConnection(UserConnection botConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _botGroupName);
            _connections[Context.ConnectionId] = botConnection;
        }
    }
}
