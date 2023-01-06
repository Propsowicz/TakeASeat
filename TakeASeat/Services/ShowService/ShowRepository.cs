using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;
using System.Drawing.Printing;
using System.Linq;
//using System.Data.Entity;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.ShowService
{
    public class ShowRepository : IShowRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public ShowRepository(DatabaseContext context, IMapper mapper)
        {
            _context= context;
            _mapper= mapper;
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

        public async Task<IPagedList<GetShowsDTO>> GetShows(int pageNumber, int pageSize)
        {
            return await _context.Shows
                .AsNoTracking()
                .Where(s => s.Date > DateTime.Now)                
                .OrderBy(s => s.Date)
                .Select(s => 
                new GetShowsDTO
                {
                    Id = s.Id,
                    IsReadyToSell= s.IsReadyToSell,
                    Date= s.Date,
                    Description= s.Description,
                    EventName = s.Event.Name,   
                    EventId = s.EventId,
                    EventPlace = s.Event.Place,
                    EventSlug = s.Event.EventSlug,
                    EventType = s.Event.EventType.Name,                    
                    EventTags = s.Event.EventTags.Select(t => t.EventTag.TagName).ToList(),
                    SeatsLeft = s.Seats.Where(s => s.ReservationId == null).Count()                    
                })
                .ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<IPagedList<GetShowsByEventDTO>> GetShowsByEvent(int eventId)
        {
            return await _context.Shows
                    .AsNoTracking()
                    .Where(s => s.EventId == eventId
                    && s.Date > DateTime.UtcNow)
                    .Select(s => 
                    new GetShowsByEventDTO
                    {
                        Id = s.Id,
                        Date= s.Date,
                        Description= s.Description,
                        EventSlug = s.Event.EventSlug,
                        SeatsLeft = s.Seats.Where(s => s.ReservationId == null).Count()
                    })
                    .OrderBy(s => s.Date)                    
                    .ToPagedListAsync(1, 5);
        }

        public async Task<IPagedList<GetShowsDTO>> GetShowsByEventTag(RequestTagsParams requestParams)
        {   
            var eventTagName = requestParams.EventTagName;

            var tagsList = await _context.EventTagEventM2M
                .Where(t => t.EventTag.TagName == eventTagName)
                .Select(t => t.EventId)
                .ToListAsync();

            return await _context.Shows
            .AsNoTracking()
            .Where(s => tagsList.Contains(s.Event.Id))
            .Select(s => 
                new GetShowsDTO
                {
                    Id = s.Id,
                    IsReadyToSell = s.IsReadyToSell,
                    Date = s.Date,
                    Description = s.Description,
                    EventName = s.Event.Name,
                    EventId = s.EventId,
                    EventPlace = s.Event.Place,
                    EventSlug = s.Event.EventSlug,
                    EventType = s.Event.EventType.Name,
                    EventTags = s.Event.EventTags.Select(t => t.EventTag.TagName).ToList(),
                    SeatsLeft = s.Seats.Where(s => s.ReservationId == null).Count()
                })
            .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);

        }

        public async Task CreateShow(CreateShowDTO showDTO)
        {
            var show = _mapper.Map<Show>(showDTO);
            await _context.Shows.AddAsync(show);
            await _context.SaveChangesAsync();
        }
    }
}
