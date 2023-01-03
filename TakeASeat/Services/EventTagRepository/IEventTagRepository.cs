using TakeASeat.Data;

namespace TakeASeat.Services.EventTagRepository
{
    public interface IEventTagRepository
    {
        Task<IList<EventTag>> getEventTags();
    }
}
