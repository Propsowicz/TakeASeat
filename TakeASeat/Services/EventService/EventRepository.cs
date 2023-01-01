using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.EventService
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _context;
        private Expression<Func<Event, string>> _orderBy;
        public EventRepository(DatabaseContext context)
        {
            _context = context;
        }
                
        public async Task<IPagedList<Event>> GetEvents(RequestEventParams requestParams)
        {
            var query = _context.Events
                            .AsNoTracking();
                            
            if (requestParams.EventTypes.Count != 0)
            {
                query = query
                        .Where(e => requestParams.EventTypes.Contains(e.EventType.Name))
                        .Where(e => e.Name.Contains(requestParams.SearchString));
            }           
            else
            {
                query = query
                         .Where(e => e.Name.Contains(requestParams.SearchString));
            }

            switch (requestParams.OrderBy)
            {
                case "name":
                    query = query
                            .OrderBy(e => e.Name);
                    break;
                case "-name":
                    query = query
                            .OrderByDescending(e => e.Name);
                    break;
            }

            return await query.ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task<int> GetEventRecordsNumber()
        {
            return await _context.Events.CountAsync();
        }
    }
}
