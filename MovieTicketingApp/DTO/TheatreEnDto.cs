using MovieTicketingApp.Models;

namespace MovieTicketingApp.DTO
{
    public class TheatreEnDto
    {
        public int Id { get; set; }

        public string NameEn { get; set; }

        public Location Location { get; set; }

        public string Type { get; set; }
    }
}
