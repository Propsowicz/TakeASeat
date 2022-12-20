using System.Linq.Expressions;
using TakeASeat.Data;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services
{
    public interface IEventRepository
    {

        Task<IPagedList<Event>> GetPaginatedAllWithoutPastShowsOrderAsc(RequestParams _requestParams, Expression<Func<Event, string>> orderBy = null);
        Task<IPagedList<Event>> GetPaginatedAllWithoutPastShowsOrderDesc(RequestParams _requestParams, Expression<Func<Event, string>> orderBy = null);

    }
}
