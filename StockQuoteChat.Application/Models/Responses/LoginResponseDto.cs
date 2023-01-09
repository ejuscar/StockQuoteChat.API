using StockQuoteChat.Application.Entities;

namespace StockQuoteChat.Application.Models.Responses
{
    public class LoginResponseDto
    {
        public LoginResponseDto(User user, string token)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Token = token;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
