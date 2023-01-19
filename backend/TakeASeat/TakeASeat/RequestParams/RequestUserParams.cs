namespace TakeASeat.RequestUtils
{
    public class RequestUserParams
    {
        const int MAX_PAGE_SIZE = 20;
        private int _deafultPageSize = 20;
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
    }
}
