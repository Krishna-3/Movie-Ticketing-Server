namespace MovieTicketingApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public Seat Seat { get; set; }

        public DateTime Time { get; set; }

        public MovieTheatre MovieTheatre { get; set; }

        public User User { get; set; }
    }
}
