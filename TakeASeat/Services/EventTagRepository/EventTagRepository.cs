using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;

namespace TakeASeat.Services.EventTagRepository
{
    public class EventTagRepository : IEventTagRepository
    {
        private readonly DatabaseContext _context;
        public EventTagRepository(DatabaseContext context)
        {
            _context= context;
        }

        public async Task<IList<EventTag>> getEventTags()
        {
            return await _context.EventTags
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}
