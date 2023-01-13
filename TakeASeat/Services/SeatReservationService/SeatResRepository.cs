using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.Services._Utils;
using TakeASeat.Services.SeatService;

namespace TakeASeat.Services.SeatReservationService
{
    public class SeatResRepository : ISeatResRepository
    {
        private readonly DatabaseContext _context;
        private readonly ISeatRepository _seatRepository;
        public SeatResRepository(DatabaseContext context, ISeatRepository seatRepository)
        {
            _context= context;
            _seatRepository= seatRepository;
        }
        public async Task CreateSeatReservation(string userId, IEnumerable<Seat> seats)
        {
            if (userId == null || userId == string.Empty)
            {
                return;
            }
            var reservation = await _context.SeatReservation
                .AddAsync(new SeatReservation
                {
                    isReserved = true,
                    ReservedTime= DateTime.UtcNow,
                    UserId= userId,
                });
            await _context.SaveChangesAsync();
            await SetReservation(seats, reservation.Entity.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seatReservationId"></param>
        /// <returns></returns>

        public async Task DeleteSeatReservation(int seatReservationId)
        {
            var query = await _context.SeatReservation
                                .Where(r => r.Id == seatReservationId)
                                .FirstOrDefaultAsync(); 
                        
            await RemoveReservation(seatReservationId);

            ArgumentNullException.ThrowIfNull(query);
            _context.Remove(query);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSeatReservations(IEnumerable<SeatReservation> seatReservations)
        {
            
            var listOfReservations = seatReservations.Select(r => r.Id).ToList();
            await RemoveMultipleReservation(listOfReservations);

            _context.RemoveRange(seatReservations);
            await _context.SaveChangesAsync();
        }

        public async void DeleteEmptyReservation(int seatReservationId)
        {
            var reservation = _context.SeatReservation
                                .Include(r => r.Seats)
                                .FirstOrDefault(r => r.Id == seatReservationId);

            ArgumentNullException.ThrowIfNull(reservation);

            if (reservation.Seats.Count == 0)
            {
                _context.SeatReservation.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Seat[]>> GetSeats(int showId)
        {
            return await _context.Seats
                    .Where(s => s.ShowId == showId)
                    .GroupBy(s => s.Row)
                    .Select(grp => grp.ToArray())
                    .ToListAsync();
        }

        public async Task RemoveMultipleReservation(List<int> reservationIds)
        {
            //string WhereConditions = string.Empty;
            //for (var i = 0; i < reservationIds.Count; i++)
            //{
            //    if (i == reservationIds.Count - 1)
            //    {
            //        WhereConditions += $"ReservationId = {reservationIds[i]}";
            //    }
            //    else
            //    {
            //        WhereConditions += $"ReservationId = {reservationIds[i]} OR ";
            //    }
            //}
            //                                                                                                                      not tested

            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync(
                //$"UPDATE Seats SET ReservationId = NULL WHERE {WhereConditions}"
                $"UPDATE Seats SET ReservationId = NULL WHERE {RawSqlHelper.WHERE_ReservationId_is_Id(reservationIds)}"
                );
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RemoveReservation(int reservationId)
        {
            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE Seats SET ReservationId = NULL WHERE ReservationId = {reservationId}"
                );
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RemoveSingleSeatFromOrder(int reservationId)
        {
            var seat = await _context.Seats
                    .Where(s => s.Id == reservationId)
                    .FirstOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(seat);
            seat.ReservationId = null;
            await _context.SaveChangesAsync();

            // DeleteEmptyReservation() checks if lastly deleted seat was the only seat left for the reservation.
            // If it was, so there is no more seats -> delete the reservation
            DeleteEmptyReservation(reservationId);
        }

        public async Task SetReservation(IEnumerable<Seat> seats, int? ReservationId)
        {
            //string WhereConditions = string.Empty;
            //var listedSeats = seats.ToList();
            //for (var i = 0; i < listedSeats.Count; i++)
            //{
            //    if (i == listedSeats.Count - 1)
            //    {
            //        WhereConditions += $"Id = {listedSeats[i].Id}";
            //    }
            //    else
            //    {
            //        WhereConditions += $"Id = {listedSeats[i].Id} OR ";
            //    }
            //}
            //                                                                                                                          not tested
            await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync(
                //$"UPDATE Seats SET ReservationId = {ReservationId} WHERE {WhereConditions}"
                $"UPDATE Seats SET ReservationId = {ReservationId} WHERE {RawSqlHelper.WHERE_Id_is_SeatId(seats)}"
                );
            await _context.Database.CommitTransactionAsync();
        }
    }
}
