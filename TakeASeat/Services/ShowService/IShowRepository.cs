using TakeASeat.Data;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.ShowService
{
    public interface IShowRepository
    {
        Task<IPagedList<Show>> GetClosestShows();
        Task<Show> GetShowDetails(int id);

        Task SetShowReadyToSell(int id);

    }
}
