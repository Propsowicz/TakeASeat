namespace TakeASeat.RequestUtils
{
    public class RequestShowParams
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
    }

    public class DeleteShowParams
    {
        public int ShowId { get; set; }
    }
}
