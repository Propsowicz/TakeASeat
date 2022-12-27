using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.Services.SeatService;

namespace TakeASeat.Services.SeatReservationService
{
    public class SeatResRepository : ISeatResRepository
    {
        private readonly DatabaseContext _context;
        private readonly ISeatRepository _seatRepository;
        private readonly IMapper _mapper;
        public SeatResRepository(DatabaseContext context, ISeatRepository seatRepository, IMapper mapper)
        {
            _context= context;
            _seatRepository= seatRepository;
            _mapper= mapper;
        }
        public async Task CreateSeatReservations(string buyerId, int eventId, IEnumerable<Seat> seats)
        {
            await _seatRepository.SetReservation(seats);
            
            foreach(var seatRes in seats)
            {
                await _context.SeatReservation
                .AddAsync(new SeatReservation { EventId = eventId, 
                                                UserId = buyerId, 
                                                SeatId = seatRes.Id });
            }

            await _context.SaveChangesAsync();

        }

        
    }
}
