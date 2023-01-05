using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.Models;
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
        public async Task<IPagedList<Event>> GetEventsByUser(RequestEventParams requestParams, string userName)
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

            return await query.ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }


        public async Task<int> GetEventRecordsNumber()
        {
            return await _context.Events.CountAsync();
        }

        public async Task<Event> CreateEvent(CreateEventDTO eventDTO)
        {
            var eventName = eventDTO.Name;
            var eventObj = _mapper.Map<Event>(eventDTO);
            eventObj.EventSlug = eventName.ToLower().Replace(" ", "-");

            _context.Events.Add(eventObj);
            await _context.SaveChangesAsync();

            return eventObj;
        }
        public async Task CreateEventWithTags(CreateEventDTO eventDTO, List<GetEventTagDTO> eventTagsDTO)
        {
            var createdEvent = await CreateEvent(eventDTO);
            var response = await _eventTagRepository.AddEventTag(eventTagsDTO, createdEvent.Id);
            
        }


    }
}
