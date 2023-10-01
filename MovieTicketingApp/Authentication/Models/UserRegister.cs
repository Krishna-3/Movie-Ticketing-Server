using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Authentication.Models
{
    public class UserRegister
    {
        [RegularExpression("^[A-Za-z][A-Za-z0-9_]{7,29}$")]
        public string Username { get; set; }

        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$")]
        public string Password { get; set; }
        
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
