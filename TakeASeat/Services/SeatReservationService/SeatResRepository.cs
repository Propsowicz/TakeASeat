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

        public async Task DeleteSeatReservation(IEnumerable<SeatReservation> seatReservation)
        {
            foreach (var reservation in seatReservation)
            {
                var listOfSeats = reservation.Seats.ToList();
                foreach (var seat in listOfSeats)
                {
                    seat.ReservationId = null;
                }
                
                //_context.Remove(reservation);
            }
            _context.SaveChanges();
            _context.RemoveRange(seatReservation);
        }
    }
}
