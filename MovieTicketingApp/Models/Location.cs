using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Models
{
    public class Location
    {
        public int Id { get; set; }

        [RegularExpression("^[a-zA-Z]{2,29}$")]
        public string City { get; set; }
    }
}
