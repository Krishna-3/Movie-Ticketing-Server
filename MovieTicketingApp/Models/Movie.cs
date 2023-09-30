using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,29}$")]
        public string TitleEn{ get; set; }

        public string TitleHi { get; set; }

        public string TitleTe{ get; set; }

        [RegularExpression("^[a-zA-Z 0-9]{2,}$")]
        public string DescriptionEn{ get; set; }

        public string DescriptionHi{ get; set; }

        public string DescriptionTe{ get; set; }

        [RegularExpression("^[a-zA-Z]{2,20}$")]
        public string LanguageEn{ get; set; }

        public string LanguageHi{ get; set; }

        public string LanguageTe{ get; set; }

        [Range(1,5)]
        public float Rating{ get; set; }

        public byte[] Photo { get; set; }   
    }
}
