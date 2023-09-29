using AutoMapper;
using MovieTicketingApp.DTO;
using MovieTicketingApp.Models;

namespace MovieTicketingApp.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Movie, MovieEnDto>();
            CreateMap<Movie, MovieHiDto>();
            CreateMap<Movie, MovieTeDto>();
            CreateMap<Theatre, TheatreEnDto>();
            CreateMap<Theatre, TheatreHiDto>();
            CreateMap<Theatre, TheatreTeDto>();
            CreateMap<Ticket, TicketDto>();
            CreateMap<TheatreName, Theatre>();
        }
    }
}
