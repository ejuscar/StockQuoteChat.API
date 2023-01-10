using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StockQuoteChat.API.Models;
using StockQuoteChat.Application.Entities;
using StockQuoteChat.Application.Models;
using StockQuoteChat.Application.Models.Responses;
using StockQuoteChat.Infrastructure.Repositories.Interfaces;
using System.Text;
using System.Text.Json;

namespace StockQuoteChat.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string _botUser;
        private readonly string _command;
        private readonly IDictionary<string, UserConnection> _connections;
        private readonly HttpClient _httpClient;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRoomRepository _userRoomRepository;

        public ChatHub(IDictionary<string, UserConnection> connections, IMessageRepository messageRepository, IUserRoomRepository userRoomRepository)
        {
            _botUser = "MyChat Bot";
            _connections = connections;
            _command = "/stock=";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7047");
            _messageRepository = messageRepository;
            _userRoomRepository = userRoomRepository;
        }

        [Authorize]
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                Clients.Group(userConnection.Room.Id.ToString())
                    .SendAsync("ReceiveMessage", new ChatMessageResponseDto($"{userConnection.User.FirstName} {userConnection.User.LastName} has left", _botUser));

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
                await _messageRepository.Insert(messageEntity);

                await Clients.Group(userConnection.Room.Id.ToString())
                    .SendAsync("ReceiveMessage", new ChatMessageResponseDto(messageEntity, userConnection.User.GetUserName()));

                SendCommand(message, userConnection);
            }
        }

        [Authorize]
        public async Task JoinRoom(UserConnection userConnection)
        {
            if (_userRoomRepository.Insert(userConnection.User, userConnection.Room))
            {
                await AddUserToRoom(userConnection);

                var messagesLoaded = _messageRepository.GetByRoom(userConnection.Room.Id)
                    .Include(m => m.UserRoom.User)
                    .Select(m => new ChatMessageResponseDto(m, m.UserRoom.User.GetUserName()))
                    .ToList();

                await Clients
                    .Clients(Context.ConnectionId)
                    .SendAsync("LoadMessages", messagesLoaded);

                await Clients
                    .Group(userConnection.Room.Id.ToString())
                    .SendAsync("ReceiveMessage", new ChatMessageResponseDto($"{userConnection.User.GetUserName()} has joined {userConnection.Room.Name}", _botUser));
            }

            else
            {
                throw new Exception("Error while trying to join in room");
            }
        }

        [Authorize]
        public async Task AddUserToRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room.Id.ToString());
            _connections[Context.ConnectionId] = userConnection;
        }

        private async Task SendCommand(string message, UserConnection userConnection)
        {
            if (message.ToLower().Contains(_command))
            {
                var stockCode = message.ToLower()
                    .Split(" ")
                    .First(w => w.Contains(_command))
                    ?.Replace(_command, "")!;

                if (string.IsNullOrEmpty(stockCode))
                    await Clients.Group(userConnection.Room.Id.ToString())
                        .SendAsync("ReceiveMessage", _botUser, "Stock Code cannot be empty.");

                ChatBotRequestDto chatBotRequest = new ChatBotRequestDto(userConnection.Room.Id.ToString(), stockCode);

                var requestContent = new StringContent(JsonSerializer.Serialize(chatBotRequest), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("chatbot", requestContent);

                if (!response.IsSuccessStatusCode)
                    await Clients.Group(userConnection.Room.Id.ToString())
                        .SendAsync("ReceiveMessage", _botUser, "An error occurred while trying to get the stock quote. Try again later.");
            }
        }
    }
}
