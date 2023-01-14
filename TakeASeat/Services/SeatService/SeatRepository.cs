﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Services.SeatReservationService;
using X.PagedList;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.IdentityModel.Tokens;

namespace TakeASeat.Services.SeatService
{
    public class SeatRepository : ISeatRepository
    {
        private readonly DatabaseContext _context;

        public SeatRepository(DatabaseContext context)
        {
            _context= context;   
        }

        public async Task CreateMultipleSeats(IEnumerable<Seat> seats)
        {
            if (seats.IsNullOrEmpty())
            {
                return;
            }
            await _context.Seats.AddRangeAsync(seats);
            await _context.SaveChangesAsync();            
        }
              
        public async Task<IList<Seat[]>> GetSeats(int showId)
        {           
            return await _context.Seats
                    .AsNoTracking()
                    .Where(s => s.ShowId == showId)
                    .GroupBy(s => s.Row)
                    .Select(grp => grp.ToArray())
                    .ToListAsync();
        }
       
    }
}
