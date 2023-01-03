using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.ShowService
{
    public interface IShowRepository
    {
        //Task<IPagedList<Show>> GetShows(int pageNumber, int pageSize);
        Task<int> GetShowRecordNumber();
        Task<Show> GetShowDetails(int id);
        Task SetShowReadyToSell(int id);
        Task<IPagedList<GetShowsDTO>> GetShows(int pageNumber, int pageSize);
        Task<IPagedList<GetShowsByEventDTO>> GetShowsByEvent(int eventId);
        Task<IPagedList<GetShowsDTO>> GetShowsByEventTag(RequestTagsParams requestParams);
    }
}
