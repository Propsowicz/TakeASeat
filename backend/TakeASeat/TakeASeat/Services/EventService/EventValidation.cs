namespace TakeASeat.Services.EventService
{
    public class EventValidation
    {
        public static bool IsAnyShowReadyToSell(int readyToSellShowsCount)
        {            
            return readyToSellShowsCount > 0;
        }
        
    }
}
