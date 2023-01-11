using StockQuoteChat.Infrastructure.Repositories.Interfaces;
using StockQuoteChat.Infrastructure.Repositories;
using StockQuoteChat.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StockQuoteChat.Application.Models;

namespace StockQuoteChat.API
{
    public static class IoC
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            // DbContext
            services.AddDbContext<ChatDbContext>(
                    options => options.UseNpgsql(ConfigurationHelper.Config.GetConnectionString("DefaultConnection")));

            // Save the connectionId for the user
            services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());

            services.AddScoped<IUserRoomRepository, UserRoomRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
        }
    }
}
