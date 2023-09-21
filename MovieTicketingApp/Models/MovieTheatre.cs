namespace MovieTicketingApp.Models
{
    public class MovieTheatre
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int TheatreId { get; set; }

        public Movie Movie { get; set; }
    
        public Theatre Theatre { get;}
    }
}
