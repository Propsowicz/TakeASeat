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

        private List<int> getTagList(RequestParams requestParams)
        {
            return _context.EventTagEventM2M
                .Where(src => src.EventTag.TagName
                .Equals(requestParams.SearchString))
                .Select(t => t.EventId).ToList();
        }


        public async Task<IPagedList<Event>> GetPaginatedAllWithoutPastShows(RequestParams requestParams) // without past shows not done yet!!!!!!
        {
            var typesList = requestParams.FilterString;
            var tagIdList = getTagList(requestParams);

            var orderBy = requestParams.Order;
            switch (orderBy)
            {
                case "name":
                case "-name":
                    _orderBy = o => o.Name;
                    break;
                case "creator":
                case "-creator":
                    _orderBy = o => o.Creator.UserName;
                    break;
            }

            var query = _context.Events
                .AsNoTracking()
                .Include(et => et.EventTags)
                    .ThenInclude(etn => etn.EventTag)
                .Include(etyp => etyp.EventType)
                .Include(ecr => ecr.Creator)
                .Include(es => es.Shows)
                .Where(search => search.Name.Contains(requestParams.SearchString)
                || tagIdList.Contains(search.Id))
                .Where(search => typesList.Contains(search.EventType.Name));

            if (Convert.ToString(requestParams.Order[0]) != "-")
            {
                return await query
                    .OrderBy(_orderBy)
                    .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
            }
            else
            {
                return await query
                    .OrderByDescending(_orderBy)
                    .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
            }
        }


    }
}
