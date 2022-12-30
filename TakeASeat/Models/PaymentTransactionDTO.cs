namespace TakeASeat.Models
{
    public class PaymentParamsDTO
    {
        public string amount { get; set; }
        public string currency { get; set; } = "USD";
        public string description { get; set; }
        public string id { get; set; }          
        public string paramsList { get; set; } = "amount;currency;description;id;type;url";
        public string type { get; set; } = "0";
        public string url { get; set; } = "https://www.example.com/thanks_page.php";
    }

    public class PaymentDataDTO : PaymentParamsDTO
    {        
        public string chk { get; set; } = string.Empty;

    }

    public class FakePaymentParamsDTO   // can be deleted
    {
        public string amount { get; set; } = "98.53";
        public string currency { get; set; } = "PLN";
        public string description { get; set; } = "Order123";
        public string id { get; set; } = "123456";    
        public string paramsList { get; set; } = "amount;currency;description;id;type;url";
        public string type { get; set; } = "0";
        public string url { get; set; } = "https://www.example.com/thanks_page.php";
    }
}
