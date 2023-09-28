using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.DTO
{
    public class MovieDescription
    {
        [RegularExpression("^[a-zA-Z 0-9]{2,}$")]
        public string DescriptionEn{ get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,}$")]
        public string DescriptionHi{ get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,}$")]
        public string DescriptionTe{ get; set; }
    }
}
