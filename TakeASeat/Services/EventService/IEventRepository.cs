using System.Linq.Expressions;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.EventService
{
    public interface IEventRepository
    {
        Task<Event> getEvent(int id);
        Task<IPagedList<Event>> getEvents(RequestEventParams requestParams);
        Task<IPagedList<Event>> getEventsByUser(RequestEventParams requestParams, string userName);
        Task<int> getEventRecordsNumber();

        Task<Event> createEvent(CreateEventDTO eventDTO);
        Task<Event> editEvent(EditEventDTO eventDTO);
        Task createEventWithTags(CreateEventDTO eventDTO, List<GetEventTagDTO> eventTagsDTO);
        Task editEventWithTags(EditEventDTO eventDTO, List<GetEventTagDTO> eventTagsDTO);
        Task deleteEvent(int eventId);
    }
}
