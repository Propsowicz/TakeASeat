using AutoMapper;
using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Event, CreateEventDTO>().ReverseMap();
            CreateMap<Event, GetEventDTO>().ReverseMap();
            CreateMap<Event, GetEventDetailsDTO>().ReverseMap();

            CreateMap<EventTag, CreateEventTagDTO>().ReverseMap();
            CreateMap<EventTag, GetEventTagDTO>().ReverseMap();

            CreateMap<Seat, CreateSeatDTO>().ReverseMap();
            CreateMap<Seat, GetSeatDTO>().ReverseMap();
            CreateMap<Seat, ReserveSeatsDTO>().ReverseMap();

            CreateMap<EventTagEventM2M, CreateEventTagEventM2MDTO>().ReverseMap();
            CreateMap<EventTagEventM2M, GetEventTagEventM2MDTO>().ReverseMap();

            CreateMap<EventType, CreateEventTypeDTO>().ReverseMap();
            CreateMap<EventType, GetEventTypeDTO>().ReverseMap();

            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, GetUserDTO>().ReverseMap();

            CreateMap<Show, CreateShowDTO>().ReverseMap();
            CreateMap<Show, GetShowDTO>().ReverseMap();
            CreateMap<Show, GetShowDetailsDTO>().ReverseMap();
        }
    }
}
