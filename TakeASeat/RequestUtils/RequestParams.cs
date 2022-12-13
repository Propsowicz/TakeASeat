using X.PagedList;

namespace TakeASeat.RequestUtils

{
    public class RequestParams
    {
        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize { get; set; } = 5;

        public int PageSize {
            get { return _pageSize; }
            set { _pageSize = (maxPageSize < value ? maxPageSize : value);}
        }
    }
}
