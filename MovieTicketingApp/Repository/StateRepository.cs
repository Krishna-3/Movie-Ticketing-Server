using MovieTicketingApp.Data;
using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class StateRepository : IStateRepository
    {
        private static State _state;
        private static DataContext _context;

        public StateRepository(State state, DataContext context)
        {
            _state = state;
            _context = context;
        }

        public List<string> GetAllowedLanguageCodes()
        {
            return _state.LanguageCode;
        }

        public State GetState(int userId)
        {
            return _context.States.FirstOrDefault(s => s.UserId == userId);                        
        }

        public Dictionary<string, string> GetTimings()
        {
            return _state.timings;
        }

        public bool SetLanguage(string language, int userId)
        {
            var state = _context.States.First(s => s.UserId == userId);

            state.preferredLanguage = language;

            _context.Update(state);

            return Save();
        }

        public bool SetLocation(string location, int userId)
        {
            var state = _context.States.First(s => s.UserId == userId);

            state.selectedLocation= location;

            _context.Update(state);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }

        public bool StateExists(int userId)
        {
            var state = _context.States.FirstOrDefault(s => s.UserId == userId);

            if(state == null)
                return false;
            return true;
        }

        public bool CreateState(int userId)
        {
            var state = new State();
            state.UserId = userId;
            state.selectedLocation = "";

            _context.Add(state);

            return Save();
        }
    }
}
