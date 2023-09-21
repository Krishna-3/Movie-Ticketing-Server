namespace MovieTicketingApp.Models
{
    public class Theatre
    {
        public int Id { get; set; }

        public string NameEn { get; set; }

        public string NameHin { get; set; }

        public string NameTel{ get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
