using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.Models
{
    public class Theatre
    {
        public int Id { get; set; }

        [RegularExpression("^[a-zA-Z ]{2,29}$")]
        public string NameEn { get; set; }

        public string NameHi { get; set; }

        public string NameTe{ get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
