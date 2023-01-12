using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.EventTagRepository
{
    public interface IEventTagRepository
    {
        Task<IList<EventTag>> getEventTags();
        Task RemoveEventTags(int EventId);
        Task AddEventTags(List<GetEventTagDTO> eventTagsDTO, int eventId);
    }
}
