using System.Runtime.CompilerServices;

namespace StockQuoteChat.Application.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            UserRooms = new HashSet<UserRoom>();
            IsBot = false;
        }

        public User(string firstName, string lastName, string email, string password, ICollection<UserRoom> userRooms, bool isBot = false)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            UserRooms = userRooms;
            IsBot = isBot;
        }

        public string GetUserName()
        {
            return $"{FirstName} {LastName}";
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsBot { get; set; }
        public virtual ICollection<UserRoom> UserRooms { get; set; }
    }
}
