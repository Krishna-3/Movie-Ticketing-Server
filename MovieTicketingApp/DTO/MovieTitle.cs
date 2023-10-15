using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.DTO
{
    public class MovieTitle
    {
        [RegularExpression("^[a-zA-Z 0-9]{2,29}$")]
        public string TitleEn { get; set; }

        public string TitleHi { get; set; }

        public string TitleTe { get; set; }
    }
}
