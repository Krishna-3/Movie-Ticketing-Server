using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketingApp.Models
{
    public class MovieLocation
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int LocationId{ get; set; }

        public Movie Movie { get; set; }

        public Location Location { get; set; }
    }
}
