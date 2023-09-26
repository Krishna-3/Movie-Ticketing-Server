namespace MovieTicketingApp.Models
{
    public class Theatre
    {
        public int Id { get; set; }

        public string NameEn { get; set; }

        public string NameHi { get; set; }

        public string NameTe{ get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }
    }
}
