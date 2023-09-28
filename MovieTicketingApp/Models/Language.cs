using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Models
{
    public class Language
    {
        public List<string> LanguageCode = new(){ "en", "hi", "te"};

        [RegularExpression("^[a-zA-Z]{2,2}$")]
        public string preferredLanguage{ get; set; }
    }
}
