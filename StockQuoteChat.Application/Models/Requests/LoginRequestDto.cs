using System.ComponentModel.DataAnnotations;

namespace StockQuoteChat.Application.Models.Requests
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
