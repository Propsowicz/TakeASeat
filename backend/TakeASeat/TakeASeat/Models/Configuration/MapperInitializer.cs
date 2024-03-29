﻿using AutoMapper;
using TakeASeat.Data;

namespace TakeASeat.Models.Configuration
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Event, CreateEventDTO>().ReverseMap();
            CreateMap<Event, GetEventDTO>().ReverseMap();
            CreateMap<Event, EditEventDTO>().ReverseMap();
            CreateMap<Event, GetEventWithListOfShowsDTO>().ReverseMap();
            CreateMap<Event, GetEventDetailsDTO>().ReverseMap();
            CreateMap<Event, GetEventDetailsToShowDTO>().ReverseMap();

            CreateMap<EventTag, CreateEventTagDTO>().ReverseMap();
            CreateMap<EventTag, GetEventTagDTO>().ReverseMap();

            CreateMap<Seat, CreateSeatDTO>().ReverseMap();
            CreateMap<Seat, GetSeatDTO>().ReverseMap();
            CreateMap<Seat, ReserveSeatsDTO>().ReverseMap();
            CreateMap<Seat, GetReservedSeatsDTO>().ReverseMap();

            CreateMap<EventTagEventM2M, CreateEventTagEventM2MDTO>().ReverseMap();
            CreateMap<EventTagEventM2M, GetEventTagEventM2MDTO>().ReverseMap();

            CreateMap<EventType, CreateEventTypeDTO>().ReverseMap();
            CreateMap<EventType, GetEventTypeDTO>().ReverseMap();

            CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, GetUserDTO>().ReverseMap();

            CreateMap<Show, CreateShowDTO>().ReverseMap();
            CreateMap<Show, GetShowDTO>().ReverseMap();
            CreateMap<Show, GetShowDetailsDTO>().ReverseMap();
            CreateMap<Show, GetClosestShow>().ReverseMap();

            CreateMap<SeatReservation, GetSeatReservationDTO>().ReverseMap();

            CreateMap<PaymentTransaction, CreatePaymentTranscationDTO>().ReverseMap();
            CreateMap<PaymentTransaction, GetPaymentTranscationDTO>().ReverseMap();
        }
    }
}
