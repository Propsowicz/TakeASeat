using TakeASeat.Data;

namespace TakeASeat.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Event> Events { get; }
        IGenericRepository<EventTag> EventTags { get; }
        IGenericRepository<Seat> Seats { get; }
        IGenericRepository<EventTagEventM2M> EventTagEventM2M { get; }
        IGenericRepository<EventType> EventTypes { get; }
        IGenericRepository<Show> Show { get; }

        Task Save();
    }
}
