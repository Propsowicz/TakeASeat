﻿using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using X.PagedList;

namespace TakeASeat.Services.ShowService
{
    public class ShowRepository : IShowRepository
    {
        private readonly DatabaseContext _context;
        public ShowRepository(DatabaseContext context)
        {
            _context= context;
        }
        public async Task<IPagedList<Show>> GetClosestShows()
        {           
            return await _context.Shows
                .AsNoTracking()
                .Where(s => s.Date > DateTime.Now)
                .Include(sh => sh.Event)
                    .ThenInclude(e => e.EventType)
                 .Include(sh => sh.Event)
                    .ThenInclude(e => e.Creator)
                .Include(sh => sh.Event)
                    .ThenInclude(e => e.EventTags)
                        .ThenInclude(t => t.EventTag)
                .OrderBy(s => s.Date)
                .ToPagedListAsync(1, 5); 
        }

        public async Task<Show> GetShowDetails(int id)
        {
            return await _context.Shows
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Include(sh => sh.Event)
                    .ThenInclude(e => e.EventType)
                 .Include(sh => sh.Event)
                    .ThenInclude(e => e.Creator)
                .Include(sh => sh.Event)
                    .ThenInclude(e => e.EventTags)
                        .ThenInclude(t => t.EventTag)
                .FirstOrDefaultAsync();
                
        }

        public async Task SetShowReadyToSell(int id)
        {
            var show = await _context.Shows.Where(s => s.Id == id).FirstOrDefaultAsync();
            show.IsReadyToSell = true;
            await _context.SaveChangesAsync();
        }
    }
}
