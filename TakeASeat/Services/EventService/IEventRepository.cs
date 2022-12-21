using System.Linq.Expressions;
using TakeASeat.Data;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.EventService
{
    public interface IEventRepository
    {

        Task<IPagedList<Event>> GetPaginatedAllWithoutPastShows(RequestParams _requestParams);

    }
}
