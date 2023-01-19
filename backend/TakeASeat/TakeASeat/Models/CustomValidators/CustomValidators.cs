using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace TakeASeat.Models.CustomValidators
{
    public class DateFiveDaysGraterThanToday : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Date should be at least five days from today.";
        }
        
        public override bool IsValid(object? value)
        {
            if (value is not DateTime) { return false; }
            var dateValue = value as DateTime?;            
            return dateValue >= DateTime.UtcNow.AddDays(5);
        }

    }
}
