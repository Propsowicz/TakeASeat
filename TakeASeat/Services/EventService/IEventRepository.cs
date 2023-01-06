using System.Linq.Expressions;
using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.EventService
{
    public interface IEventRepository
    {
        Task<Event> GetEvent(int id);
        Task<IPagedList<Event>> GetEvents(RequestEventParams requestParams);
        Task<IPagedList<Event>> GetEventsByUser(RequestEventParams requestParams, string userName);
        Task<int> GetEventRecordsNumber();


        Task<Event> CreateEvent(CreateEventDTO eventDTO);
        Task<Event> EditEvent(EditEventDTO eventDTO);
        Task CreateEventWithTags(CreateEventDTO eventDTO, List<GetEventTagDTO> eventTagsDTO);
        Task EditEventWithTags(EditEventDTO eventDTO, List<GetEventTagDTO> eventTagsDTO);

        Task DeleteEvent(int eventId);
    }
}
