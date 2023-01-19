namespace TakeASeat.Services._Utils
{
    public class PaymentDescriptionToListOfReservationsConverter
    {

        public static List<int> Convert(string PaymentDescription)
        {
            string stringToConvert = PaymentDescription.Substring(21);
            List<int> tempList = stringToConvert
                .Split("::")
                .Where(i => int.TryParse(i, out _))
                .Select(Int32.Parse).ToList();              
            return tempList;
        }
    }
}
