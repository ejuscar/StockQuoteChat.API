using StockQuoteChat.API.Hubs;
using StockQuoteChat.API.Models;
using System.Security;

var builder = WebApplication.CreateBuilder(args);
// Save the connectionId for the user
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.MapHub<ChatHub>("/chat");

app.Run();
