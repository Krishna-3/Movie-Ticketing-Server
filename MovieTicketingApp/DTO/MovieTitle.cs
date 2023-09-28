using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.DTO
{
    public class MovieTitle
    {
        [RegularExpression("^[a-zA-Z 0-9]{2,29}$")]
        public string TitleEn { get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,29}$")]
        public string TitleHi { get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,29}$")]
        public string TitleTe { get; set; }
    }
}
