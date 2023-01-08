namespace TakeASeat.RequestUtils
{
    public class RequestUserParams
    {
        const int _maxPageSize = 20;
        private int _deafultPageSize = 20;
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
    }
}
