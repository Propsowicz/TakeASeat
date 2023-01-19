namespace TakeASeat.RequestUtils
{
    public class RequestEventParams
    {
        const int MAX_PAGE_SIZE = 20;
        private int _deafultPageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get => _deafultPageSize;
            set
            {
                if (value < MAX_PAGE_SIZE)
                {
                    _deafultPageSize = value;
                }
            }
        }
        public string SearchString { get; set; } = string.Empty;
        public List<string> EventTypes { get; set; } = new List<string>();
        public string OrderBy { get; set; } = "name";
    }

    public class RequestEventDeleteParams
    {
        public int EventId { get; set; }
    }
}
