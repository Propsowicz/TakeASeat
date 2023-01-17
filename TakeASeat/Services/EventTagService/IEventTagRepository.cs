using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.EventTagRepository
{
    public interface IEventTagRepository
    {
        Task<IList<EventTag>> getEventTags();
        Task removeEventTags(int EventId);
        Task addEventTags(List<GetEventTagDTO> eventTagsDTO, int eventId);
    }
}
