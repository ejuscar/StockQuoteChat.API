namespace StockQuoteChat.Application.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            Messages = new HashSet<Message>();
        }

        public User(string firstName, string lastName, string email, string password, ICollection<Message> messages)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Messages = messages;
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
