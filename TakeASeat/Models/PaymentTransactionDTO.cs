namespace TakeASeat.Models
{
    /// <summary>
    /// Params are classes with information needed to connect with DOTPAY API
    /// </summary>
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

    public class CreatePaymentTranscationDTO
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }

    public class GetPaymentTranscationDTO : CreatePaymentTranscationDTO
    {
        public bool isAccepted { get; set; }
        public int Id { get; set; }
        public GetUserDTO User { get; set; }
    }

    public class GetTotalCostByUser
    {
        public double TotalCost { get; set; }
    }
}
