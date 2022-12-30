using TakeASeat.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TakeASeat.Models;
using Newtonsoft.Json;
using TakeASeat.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;

namespace TakeASeat.Services.PaymentService
{
    public class PaymentUtils
    {
        private readonly string _DOTPAY_PIN;
        private readonly string _DOTPAY_ID;
        private readonly List<double> _listOfPrices;
        private readonly List<SeatReservation> _seatReservations;
        private readonly PaymentParamsDTO _paymentParams;
        public PaymentUtils(string DOTPAY_PIN, string DOTPAY_ID, List<double> listOfPrices, List<SeatReservation> seatReservations)
        {
            _DOTPAY_PIN = DOTPAY_PIN;
            _DOTPAY_ID = DOTPAY_ID;
            _listOfPrices= listOfPrices;
            _seatReservations= seatReservations;

            // create Params
            _paymentParams = new PaymentParamsDTO();
            _paymentParams.amount = Convert.ToString(Math.Round(_listOfPrices.Sum(), 2));
            _paymentParams.description = $"SeatReservationsIds::";
            _paymentParams.id = _DOTPAY_ID;
            foreach(var reservation in _seatReservations)
            {
                _paymentParams.description += Convert.ToString(reservation.Id) + "::";
            }

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
}
