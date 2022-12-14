using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;
using TakeASeat.IRepository;

namespace TakeASeat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Event> _events;
        private IGenericRepository<EventTag> _eventTags;
        private IGenericRepository<Seat> _seats;
        private IGenericRepository<EventTagEventM2M> _eventM2M;
        private IGenericRepository<EventType> _eventTypes;

        public UnitOfWork(DatabaseContext context)
        {
            _context= context;
        }

        public IGenericRepository<Event> Events => _events ??= new GenericRepository<Event>(_context);

        public IGenericRepository<EventTag> EventTags => _eventTags ??= new GenericRepository<EventTag>(_context);

        public IGenericRepository<Seat> Seats => _seats ??= new GenericRepository<Seat>(_context);

        public IGenericRepository<EventTagEventM2M> EventTagEventM2M => _eventM2M ??= new GenericRepository<EventTagEventM2M>(_context);

        public IGenericRepository<EventType> EventTypes => _eventTypes ??= new GenericRepository<EventType>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this); // garbage collector
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }



}
