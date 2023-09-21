namespace MovieTicketingApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        public int SeatId{ get; set; }

        public DateTime Time { get; set; }

        public int MovieTheatreId{ get; set; }

        public int UserId { get; set; }

        public Seat Seat { get; set; }

        public MovieTheatre MovieTheatre { get; set; }

        public User User { get; set; }
    }
}
