using MovieTicketingApp.Models;

namespace MovieTicketingApp.DTO
{
    public class TheatreTeDto
    {
        public int Id { get; set; }

        public string NameTe { get; set; }

        public Location Location { get; set; }

        public string Type { get; set; }
    }
}
