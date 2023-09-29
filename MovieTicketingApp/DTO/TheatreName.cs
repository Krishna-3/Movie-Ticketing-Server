using System.ComponentModel.DataAnnotations;

namespace MovieTicketingApp.DTO
{
    public class TheatreName
    {
        [RegularExpression("^[a-zA-Z ]{2,29}$")]
        public string NameEn { get; set; }

        public string NameHi { get; set; }

        public string NameTe { get; set; }
    }
}
