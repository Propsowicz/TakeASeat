namespace TakeASeat.Services.EventService
{
    public class EventValidation
    {
        public static bool IsAnyShowReadyToSell(int readyToSellShowsCount)
        {
            if (readyToSellShowsCount > 0)
            {
                return true;
            }
            return false;
        }
        
    }
}
