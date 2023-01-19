namespace TakeASeat.RequestUtils
{
    public class RequestShowParams
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
    }

    public class DeleteShowParams
    {
        public int ShowId { get; set; }
    }
}
