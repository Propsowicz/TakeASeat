using Newtonsoft.Json;
using System.Security.Cryptography;
using TakeASeat.Data;
using TakeASeat.Models;

namespace TakeASeat.Services.PaymentService
{
    public class PaymentData
    {
        private readonly string _DOTPAY_PIN;
        private readonly string _DOTPAY_ID;
        private readonly List<double> _listOfPrices;
        private readonly List<SeatReservation> _seatReservations;
        private readonly PaymentParamsDTO _paymentParams;
        public PaymentData(string DOTPAY_PIN, string DOTPAY_ID, List<double> listOfPrices, List<SeatReservation> seatReservations)
        {
            _DOTPAY_PIN = DOTPAY_PIN;
            _DOTPAY_ID = DOTPAY_ID;
            _listOfPrices = listOfPrices;
            _seatReservations = seatReservations;

            // create Params
            _paymentParams = new PaymentParamsDTO();
            _paymentParams.amount = Convert.ToString(Math.Round(_listOfPrices.Sum(), 2));
            _paymentParams.description = "SeatReservationsIds:{";                                  
            _paymentParams.id = _DOTPAY_ID;
            foreach (var reservation in _seatReservations)
            {
                _paymentParams.description += Convert.ToString(reservation.Id) + ",";
            }
            _paymentParams.description += "}";
        }

        private string createPaymentSignature()
        {
            string paramsJSON = JsonConvert.SerializeObject(_paymentParams);
            var key = new System.Text.UTF8Encoding().GetBytes(_DOTPAY_PIN);
            var json = new System.Text.UTF8Encoding().GetBytes(paramsJSON);

            var hmacsha256 = new HMACSHA256(key);
            var hash = hmacsha256.ComputeHash(json);
            var signature = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return signature;
        }

        public PaymentDataDTO getPaymentData()
        {
            PaymentDataDTO paymentData = new PaymentDataDTO();
            paymentData.amount = _paymentParams.amount;
            paymentData.description = _paymentParams.description;
            paymentData.id = _paymentParams.id;
            paymentData.chk = createPaymentSignature();

            return paymentData;
        }        
    }
    public class PaymentServerResponse  
    {
        private readonly string _DOTPAY_PIN;
        private readonly ResponseFromPaymentTransaction _paymentResponse;

        public PaymentServerResponse(string DOTPAY_PIN, ResponseFromPaymentTransaction paymentResponse)
        {
            _paymentResponse = paymentResponse;
            _DOTPAY_PIN = DOTPAY_PIN;
        }        
        public string createResponseSignature()
        {
            string signatureString = _DOTPAY_PIN + _paymentResponse.operation_number + _paymentResponse.operation_type
                                    + _paymentResponse.operation_status + _paymentResponse.operation_amount
                                    + _paymentResponse.operation_currency + _paymentResponse.operation_original_amount
                                    + _paymentResponse.operation_original_currency + _paymentResponse.operation_datetime
                                    + _paymentResponse.control + _paymentResponse.description
                                    + _paymentResponse.email + _paymentResponse.p_info
                                    + _paymentResponse.p_email + _paymentResponse.channel;


            var stringToEncode = new System.Text.UTF8Encoding().GetBytes(signatureString);
            var noKey = new System.Text.UTF8Encoding().GetBytes("");

            var hmacsha256 = new HMACSHA256(noKey);
            var hash = hmacsha256.ComputeHash(stringToEncode);
            var signature = BitConverter.ToString(hash).Replace("-", "").ToLower();

            return signature;
        }
        private bool isPaymentSuccessfull()
        {            
            return _paymentResponse.operation_status == "completed";
        }
        private bool isPaymentResponseValid ()
        {
            return createResponseSignature() == _paymentResponse.signature;
        }

        public bool isValid()
        {            
            return isPaymentSuccessfull() && isPaymentResponseValid();
        }
    }
}
