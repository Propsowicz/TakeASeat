namespace TakeASeat.RequestUtils
{
    public class RequestEventParams
    {
        const int _maxPageSize = 20;
        private int _deafultPageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int PageSize
        {
            get
            {
                return _deafultPageSize;
            }
            set
            {
                if (value < _maxPageSize)
                {
                    _deafultPageSize = value;
                }
            }
        }
        public string SearchString { get; set; } = string.Empty;
        public List<string> EventTypes { get; set; } = new List<string>();
        public string OrderBy { get; set; } = "name";
    }
}
