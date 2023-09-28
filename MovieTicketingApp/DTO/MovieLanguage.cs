using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.DTO
{
    public class MovieLanguage
    {
        [RegularExpression("^[a-zA-Z]{2,20}$")]
        public string LanguageEn { get; set; }

        public string LanguageHi { get; set; }

        public string LanguageTe { get; set; }
    }
}
