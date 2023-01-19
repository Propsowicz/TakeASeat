using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
using TakeASeat.ProgramConfigurations.DTO;
using TakeASeat.RequestUtils;
using TakeASeat.Services.EventTagRepository;
using X.PagedList;

namespace TakeASeat.Services.EventService
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IEventTagRepository _eventTagRepository;
        public EventRepository(DatabaseContext context, IMapper mapper, IEventTagRepository eventTagRepository)
        {
            _context = context;
            _mapper = mapper;
            _eventTagRepository = eventTagRepository;
        }
                
        public async Task<IPagedList<Event>> getEvents(RequestEventParams requestParams)
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
        public async Task<IPagedList<Event>> getEventsByUser(RequestEventParams requestParams, string userName)
        {            
            var query = _context.Events
                            .AsNoTracking()                            
                            .Where(e => e.Creator.UserName == userName);

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

            return await query.Include(e => e.Shows).ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }
        public async Task<int> getEventRecordsNumber()
        {
            return await _context.Events.CountAsync();
        }
        public async Task<Event> createEvent(CreateEventDTO eventDTO)
        {
            var eventName = eventDTO.Name;
            if (eventDTO.CreatorId == null || eventDTO.Description == null ||
                eventDTO.Name == null || eventDTO.Place == null ) 
            {
                return null;
            }
            var eventObj = _mapper.Map<Event>(eventDTO);
            eventObj.EventSlug = eventName.ToLower().Replace(" ", "-");
            _context.Events.Add(eventObj);

            await _context.SaveChangesAsync();            
            return eventObj;
        }
        public async Task createEventWithTags(CreateEventDTO eventDTO, List<GetEventTagDTO> eventTagsDTO)
        {
            var createdEvent = await createEvent(eventDTO);
            await _eventTagRepository.addEventTags(eventTagsDTO, createdEvent.Id);            
        }

        public async Task<Event> getEvent(int id)
        {
            var query = await _context.Events
                            .AsNoTracking()
                            .Include(e => e.EventType)
                            .Include(e => e.EventTags)
                                .ThenInclude(t => t.EventTag)
                            .SingleOrDefaultAsync(e => e.Id == id);

            ArgumentNullException.ThrowIfNull(query);

            return query;
        }
        public async Task editEventWithTags(EditEventDTO eventDTO, List<GetEventTagDTO> eventTagsDTO)
        {
            var editedEvent = await editEvent(eventDTO);
            await _eventTagRepository.removeEventTags(eventDTO.Id);
            await _eventTagRepository.addEventTags(eventTagsDTO, editedEvent.Id);
        }
        public async Task<Event> editEvent(EditEventDTO eventDTO)
        {
            if (eventDTO.Id < 1 || eventDTO.Description == null ||
                eventDTO.Name == null || eventDTO.Place == null)
            {
                return null;
            }

            var eventObj = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventDTO.Id);
            ArgumentNullException.ThrowIfNull(eventObj);

            // fields that can be changed:
            eventObj.Name= eventDTO.Name;
            eventObj.EventSlug = eventDTO.Name.ToLower().Replace(" ", "-");
            eventObj.Description= eventDTO.Description;
            eventObj.Duration= eventDTO.Duration;
            eventObj.EventTypeId = eventDTO.EventTypeId;
            eventObj.Place = eventDTO.Place;
            eventObj.ImageUrl= eventDTO.ImageUrl;

            return eventObj;
        }
        public async Task deleteEvent(int eventId)
        {
            var queryValidation = await _context.Shows
                                    .AsNoTracking()
                                    .Where(s => s.EventId == eventId)
                                    .Where(s => s.IsReadyToSell == true)
                                    .ToListAsync();

            if (!EventValidation.IsAnyShowReadyToSell(queryValidation.Count()))
            {
                var query = await _context.Events.FirstOrDefaultAsync(e => e.Id == eventId);
                if (query == null)
                {
                    throw new NullReferenceException();
                }

                _context.Events.Remove(query);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ElementIsUsageException("Can't delete event with shows which are ready to sell.");
            }              
        }
        
    }
}
