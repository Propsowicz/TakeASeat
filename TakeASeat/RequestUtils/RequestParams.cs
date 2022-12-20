using X.PagedList;
using TakeASeat.Data;
using TakeASeat.Data.DatabaseContext;

namespace TakeASeat.RequestUtils

{
    public class RequestParams
    {        
        public int PageNumber { get; set; } = 1;

        const int maxPageSize = 20;
        private int _pageSize { get; set; } = 5;
        public int PageSize {
            get { return _pageSize; }
            set { _pageSize = (maxPageSize < value ? maxPageSize : value);}
        }
        public string SearchString { get; set; } = "";

        
        List<string> typesList { get; set; } = new List<string>() {
            "Movie", "Sport", "E-Sport"
            };
        public List<string> FilterString {
            get { return typesList; }
            set {
                if (value[0] != "all")
                {
                    typesList = value;                  
                }             
            } 
        }

        public string Order { get; set; } = "Name";
        
    }
}
