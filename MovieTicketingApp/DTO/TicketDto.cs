using MovieTicketingApp.Models;

namespace MovieTicketingApp.DTO
{
    public class TicketDto
    {
        public DateTime Time { get; set; }

        public Seat Seat { get; set; }

        public MovieTheatre MovieTheatre { get; set; }
    }
}
