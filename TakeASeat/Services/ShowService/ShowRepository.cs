using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
//using System.Data.Entity;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.RequestUtils;
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
        public async Task<IPagedList<Show>> GetShows(int pageNumber, int pageSize)
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
                .ToPagedListAsync(pageNumber, pageSize);
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

        public async Task<int> GetShowRecordNumber()
        {
            var query = await _context.Shows
                .AsNoTracking()
                .Where(s => s.Date > DateTime.Now)
                .ToListAsync();
            return query.Count();
                
        }
    }
}
