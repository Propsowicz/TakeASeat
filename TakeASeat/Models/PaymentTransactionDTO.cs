namespace TakeASeat.Models
{
    public class PaymentTransactionDTO
    {

    }

    public class PaymentDataDTO
    {
        public double Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string Description { get; set; }
        public string Id { get; set; }          // it's not Id but rather 
        public string Ignore_last_payment_channel { get; set; } = "1";
        public string ParamsList { get; set; } = "amount;currency;description;id;ignore_last_payment_channel;type;url;urlc";
        public string Type { get; set; } = "0";
        public string URL { get; set; } = "www.www.com";
        public string URLC { get; set; } = "www.www.com";
        public string PaymentSignature { get; set; } = "empty";

    }
}
