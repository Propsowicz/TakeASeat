using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _context;
        public EventRepository(DatabaseContext context)
        {
            _context = context;
        }
                    
        private List<int> getTagList(RequestParams requestParams)
        {
            return _context.EventTagEventM2M
                .Where(src => src.EventTag.TagName
                .Equals(requestParams.SearchString))
                .Select(t => t.EventId).ToList();
        }


        public async Task<IPagedList<Event>> GetPaginatedAllWithoutPastShowsOrderAsc(RequestParams requestParams,
            Expression<Func<Event, string>> orderBy = null)
        {
            var query = _context.Events
                .AsNoTracking();
                        
            var typesList = requestParams.FilterString;
            var tagIdList = getTagList(requestParams);

            if (orderBy == null)
            {
                orderBy = q => q.Name;
            }

            return await query
                .Include(et => et.EventTags)
                    .ThenInclude(etn => etn.EventTag)
                .Include(etyp => etyp.EventType)
                .Include(ecr => ecr.Creator)
                .Include(es => es.Shows)
                .Where(search => search.Name.Contains(requestParams.SearchString)
                || tagIdList.Contains(search.Id))
                .Where(search => typesList.Contains(search.EventType.Name))
                .OrderBy(orderBy)
                .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task<IPagedList<Event>> GetPaginatedAllWithoutPastShowsOrderDesc(RequestParams requestParams,
            Expression<Func<Event, string>> orderBy = null)
        {
            var query = _context.Events
                .AsNoTracking();

            var typesList = requestParams.FilterString;
            var tagIdList = getTagList(requestParams);

            if (orderBy == null)
            {
                orderBy = q => q.Name;
            }

            return await query
                .Include(et => et.EventTags)
                    .ThenInclude(etn => etn.EventTag)
                .Include(etyp => etyp.EventType)
                .Include(ecr => ecr.Creator)
                .Include(es => es.Shows)
                .Where(search => search.Name.Contains(requestParams.SearchString)
                || tagIdList.Contains(search.Id))
                .Where(search => typesList.Contains(search.EventType.Name))
                .OrderByDescending(orderBy)
                .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }
    }
}
