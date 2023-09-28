using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,29}$")]
        public string TitleEn{ get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,29}$")]
        public string TitleHi { get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,29}$")]
        public string TitleTe{ get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,}$")]
        public string DescriptionEn{ get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,}$")]
        public string DescriptionHi{ get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,}$")]
        public string DescriptionTe{ get; set; }

        [RegularExpression("^[a-zA-Z]{2,20}$")]
        public string Language{ get; set; }

        [Range(1,5)]
        public float Rating{ get; set; }
    }
}
