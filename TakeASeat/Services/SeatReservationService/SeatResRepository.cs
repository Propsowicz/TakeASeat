using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task CreateSeatReservation(string userId, IEnumerable<Seat> seats)
        {
            var reservation = await _context.SeatReservation
                .AddAsync(new SeatReservation
                {
                    isReserved = true,
                    ReservedTime= DateTime.UtcNow,
                    UserId= userId,
                });
            await _context.SaveChangesAsync();
            await _seatRepository.SetReservation(seats, reservation.Entity.Id);
        }

        

        public async Task DeleteSeatReservation(int seatReservationId)
        {
            var query = await _context.SeatReservation
                                .Where(r => r.Id == seatReservationId)
                                .FirstOrDefaultAsync(); 
                        
            await _seatRepository.RemoveReservation(seatReservationId);

            ArgumentNullException.ThrowIfNull(query);
            _context.Remove(query);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSeatReservations(IEnumerable<SeatReservation> seatReservations)
        {
            
            var listOfReservations = seatReservations.Select(r => r.Id).ToList();
            await _seatRepository.RemoveMultipleReservation(listOfReservations);

            _context.RemoveRange(seatReservations);
            await _context.SaveChangesAsync();
        }


    }
}
