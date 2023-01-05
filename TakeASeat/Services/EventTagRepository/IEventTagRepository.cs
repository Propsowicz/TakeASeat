using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.EventTagRepository
{
    public interface IEventTagRepository
    {
        Task<IList<EventTag>> getEventTags();
        Task<int> AddEventTag(List<GetEventTagDTO> eventTagsDTO, int eventId);
    }
}
