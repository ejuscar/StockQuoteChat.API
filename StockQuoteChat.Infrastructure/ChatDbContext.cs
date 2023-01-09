using Microsoft.EntityFrameworkCore;
using StockQuoteChat.Application.Entities;
using System.Reflection.Emit;

namespace StockQuoteChat.Infrastructure
{
    public class ChatDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);

            builder.Entity<Message>()
                .HasOne(m => m.Room)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.RoomId);

            new ChatDbContextSeed(builder).Seed();
        }
    }
}