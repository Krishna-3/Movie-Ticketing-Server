using MovieTicketingApp.Interfaces;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Repository
{
    public class StateRepository : IStateRepository
    {
        private static State _state;

        public StateRepository(State state)
        {
            _state = state;
        }

        public List<string> GetAllowedLanguageCodes()
        {
            return _state.LanguageCode;
        }

        public string GetLanguage()
        {
            return _state.preferredLanguage;
        }

        public string GetLocation()
        {
            return _state.selectedLocation;
        }

        public Dictionary<string, string> GetTimings()
        {
            return _state.timings;
        }

        public void SetLanguage(string language)
        {
            _state.preferredLanguage = language;
        }

        public void SetLocation(string location)
        {
            _state.selectedLocation = location;
        }
    }
}
