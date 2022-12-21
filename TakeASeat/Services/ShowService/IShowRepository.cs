using TakeASeat.Data;
using TakeASeat.RequestUtils;
using X.PagedList;

namespace TakeASeat.Services.ShowService
{
    public interface IShowRepository
    {

        Task<IPagedList<Show>> GetClosestShows();


    }
}
