using Microsoft.EntityFrameworkCore;
using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;
using System.Xml.Serialization;

namespace MovieTicketingApp.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private DataContext _context;

        public SeatRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSeats()
        {
            List<char> arr = new() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M','N','O' };

            foreach (var item in arr)
            {
                for(int i =1; i<=10;i++)
                {
                    Seat seat = new();

                    seat.SeatNumber= item + i.ToString();

                    _context.Add(seat);
                }
            }

            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public IEnumerable<Seat> GetSeats()
        {
            return _context.Seats.ToList();
        }
    }
}
