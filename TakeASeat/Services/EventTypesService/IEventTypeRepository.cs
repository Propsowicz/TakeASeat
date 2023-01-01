using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.EventTypesService
{
    public interface IEventTypeRepository
    {
        Task<IEnumerable<EventType>> getEventTypes();
    }
}
