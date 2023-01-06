using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;

namespace TakeASeat.Services.EventTagRepository
{
    public class EventTagRepository : IEventTagRepository
    {
        private readonly DatabaseContext _context;
        public EventTagRepository(DatabaseContext context)
        {
            _context= context;
        }
             
        public async Task AddEventTags(List<GetEventTagDTO> eventTagsDTO, int eventId)
        {
            List<EventTagEventM2M> tagsToAdd = new List<EventTagEventM2M>();
            foreach(var tag in eventTagsDTO)
            {
                tagsToAdd.Add(new EventTagEventM2M()
                {
                    EventId= eventId,
                    EventTagId= tag.Id
                });
            }

            await _context.EventTagEventM2M.AddRangeAsync(tagsToAdd);
            await _context.SaveChangesAsync();
                        
        }

        public async Task<IList<EventTag>> getEventTags()
        {
            return await _context.EventTags
                        .AsNoTracking()
                        .ToListAsync();
        }

        public async Task RemoveEventTags(int EventId)
        {
            var query = await _context.EventTagEventM2M.Where(t => t.EventId== EventId).ToListAsync();
            _context.RemoveRange(query);
            //await _context.SaveChangesAsync();
        }
    }
}
