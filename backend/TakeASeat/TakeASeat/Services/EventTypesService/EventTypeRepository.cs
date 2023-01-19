using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;

namespace TakeASeat.Services.EventTypesService
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly DatabaseContext _context;
        public EventTypeRepository(DatabaseContext context)
        {
            _context= context;
        }
        public async Task<IEnumerable<EventType>> getEventTypes()
        {
            return await _context.EventTypes
                        .AsNoTracking()
                        .ToListAsync();
        }
    }
}
