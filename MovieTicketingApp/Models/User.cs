using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [RegularExpression("^[A-Za-z][A-Za-z0-9_]{7,29}$")]
        public string Username { get; set; }

        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$")]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Role { get; set; } = "user";
    }
}
