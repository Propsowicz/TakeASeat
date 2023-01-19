using System.ComponentModel.DataAnnotations;

namespace TakeASeat_Tests.UnitTests.Utils
{
    public class DTOValidation
    {
        public static IList<ValidationResult> CheckForErrors(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

    }
}
