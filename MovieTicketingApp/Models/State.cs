using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Models
{
    public class State
    {
        public List<string> LanguageCode = new(){ "en", "hi", "te"};

        [RegularExpression("^[a-zA-Z]{2,2}$")]
        public string preferredLanguage{ get; set; }

        [RegularExpression("^[a-zA-Z]{2,29}$")]
        public string selectedLocation {  get; set; }   
    }
}
