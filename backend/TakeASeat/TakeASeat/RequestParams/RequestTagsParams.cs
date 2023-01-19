namespace TakeASeat.RequestUtils
{
    public class RequestTagsParams
    {
        const int MAX_PAGE_SIZE = 20;
        private int defaultPageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get { return defaultPageSize; }
            set 
            { 
             if (value < MAX_PAGE_SIZE)
                {
                    defaultPageSize= value;
                }
            } 
        }
        private string _tagName;
        public string EventTagName {
            get => _tagName;
            set => _tagName = "#" + value;           
        }

    }
}
