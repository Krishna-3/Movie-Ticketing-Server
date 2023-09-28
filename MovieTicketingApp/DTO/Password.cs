using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.DTO
{
    public class Password
    {
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$")]
        public string prevPassword { get; set; }

        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$")]
        public string newPassword { get; set; }

    }
}
