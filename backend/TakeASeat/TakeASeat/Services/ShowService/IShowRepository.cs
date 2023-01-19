using TakeASeat.Data;
using TakeASeat.Models;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.ShowService
{
    public interface IShowRepository
    {
        Task<int> getShowRecordsNumber();
        Task<Show> getShowDetails(int id);
        Task setShowReadyToSell(int id);
        Task<IPagedList<GetShowsDTO>> getShows(int pageNumber, int pageSize);
        Task<IPagedList<GetShowsByEventDTO>> getShowsByEvent(int eventId);
        Task<IPagedList<GetShowsDTO>> getShowsByEventTag(RequestTagsParams requestParams);

        Task createShow(CreateShowDTO showDTO);
        Task deleteShow(int showId);
    }
}
