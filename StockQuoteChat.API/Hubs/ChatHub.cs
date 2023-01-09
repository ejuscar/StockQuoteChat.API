using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using StockQuoteChat.API.Models;
using System;
using System.Net.Http;
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

        public ChatHub(IDictionary<string, UserConnection> connections)
        {
            _botUser = "MyChat Bot";
            _connections = connections;
            _command = "/stock=";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7047");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if(_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                _connections.Remove(Context.ConnectionId);
                Clients.Group(userConnection.Room)
                    .SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has left");

                Groups.RemoveFromGroupAsync(Context.ConnectionId, userConnection.Room);
            }
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            if (_connections.TryGetValue(Context.ConnectionId, out UserConnection userConnection))
            {
                await Clients.Group(userConnection.Room)
                    .SendAsync("ReceiveMessage", userConnection.User, message);

                SendCommand(message, userConnection);
            }
        }

        [Authorize]
        public async Task JoinRoom(UserConnection userConnection)
        {
            await AddUserToRoom(userConnection);
            await Clients.Group(userConnection.Room).SendAsync("ReceiveMessage", _botUser, $"{userConnection.User} has joined {userConnection.Room}");
        }

        public async Task AddUserToRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room);
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
                    await Clients.Group(userConnection.Room)
                        .SendAsync("ReceiveMessage", _botUser, "Stock Code cannot be empty.");

                ChatBotRequestDto chatBotRequest = new ChatBotRequestDto(userConnection.Room, stockCode);

                var requestContent = new StringContent(JsonSerializer.Serialize(chatBotRequest), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("chatbot", requestContent);

                if (!response.IsSuccessStatusCode)
                    await Clients.Group(userConnection.Room)
                        .SendAsync("ReceiveMessage", _botUser, "An error occurred while trying to get the stock quote. Try again later.");
            }
        }
    }
}
