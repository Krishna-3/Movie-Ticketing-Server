namespace MovieTicketingApp.Models
{
    public class MovieWithPhoto
    {
        public IFormFile file { get; set; }

        public Movie Movie { get; set; }
    }
}
