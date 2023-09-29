using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Models
{
    public class State
    {
        public readonly Dictionary<string, string> timings = new()
        {
            {"1", "9:00AM" },
            {"2", "1:00PM" },
            {"3", "5:00PM" },
            {"4", "10:00PM" }
        };
        public readonly List<string> LanguageCode = new(){ "en", "hi", "te"};

        [RegularExpression("^[a-zA-Z]{2,2}$")]
        public string preferredLanguage{ get; set; }

        [RegularExpression("^[a-zA-Z]{2,29}$")]
        public string selectedLocation {  get; set; }   
    }
}
