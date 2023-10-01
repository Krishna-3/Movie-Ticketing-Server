using AutoMapper;
using MovieTicketingApp.Authentication.Models;
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
            CreateMap<MovieLocationDto, MovieLocation>();
            CreateMap<MovieTheatreDto, MovieTheatre>();
            CreateMap<TicketId, Ticket>();
            CreateMap<UserRegister, User>();
            CreateMap<UserLogin, User>();
        }
    }
}
