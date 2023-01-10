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
        public DbSet<UserRoom> UserRooms { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRoom>()
                .HasKey(ur => new { ur.UserId, ur.RoomId });

            builder.Entity<User>()
                .HasKey(u => u.Id);

            builder.Entity<Room>()
                .HasKey(r => r.Id);

            builder.Entity<Message>()
                .HasKey(m => m.Id);

            builder.Entity<UserRoom>()
                .HasOne(ur => ur.Room)
                .WithMany(r => r.UserRooms)
                .HasForeignKey(ur => ur.RoomId);

            builder.Entity<UserRoom>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRooms)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<Message>()
                .HasOne(m => m.UserRoom)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => new { m.UserId, m.RoomId });

            new ChatDbContextSeed(builder).Seed();
        }
    }
}