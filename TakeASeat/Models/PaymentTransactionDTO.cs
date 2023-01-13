namespace TakeASeat.Models
{
    /// <summary>
    /// Params are classes with information needed to send to DOTPAY API
    /// </summary>
    public class PaymentParamsDTO
    {
        public string amount { get; set; }
        public string currency { get; set; } = "USD";
        public string description { get; set; }
        public string id { get; set; }          
        public string paramsList { get; set; } = "amount;currency;description;id;type;url;urlc";
        public string type { get; set; } = "0";
        public string url { get; set; } = "https://www.example.com/thanks_page.php";
        public string urlc { get; set; } = "https://www.example.com/payment_reponse.php";
        //https://webhook.site/ web site to mock post endpoint
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

    public class GetPaymentTranscationDTO
    {
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public bool isAccepted { get; set; }
        public int Id { get; set; }
        public GetUserDTO User { get; set; }
    }

    public class GetTotalCostByUser
    {
        public double TotalCost { get; set; }
    }

    public class ResponseFromPaymentTransaction
    {
        public string id { get; set; }
        public string operation_number { get; set; }
        public string operation_type { get; set;}
        public string operation_status { get; set;}
        public string operation_amount { get; set;}
        public string operation_currency { get; set;}
        public string operation_original_amount { get; set;}
        public string operation_original_currency { get; set;}
        public string operation_datetime { get; set;}
        public string control { get; set;}
        public string description { get; set;}
        public string email { get; set;}
        public string p_info { get; set;}
        public string p_email { get; set;}
        public string channel { get; set;}
        public string signature { get; set; } = string.Empty;
    }
}

