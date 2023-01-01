using TakeASeat.Data;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.ShowService
{
    public interface IShowRepository
    {
        Task<IPagedList<Show>> GetShows(int pageNumber, int pageSize);
        Task<int> GetShowRecordNumber();
        Task<Show> GetShowDetails(int id);
        Task SetShowReadyToSell(int id);

    }
}
