using System.ComponentModel.DataAnnotations;

namespace Tests.Utils
{
    public class ModelValidateHelper
    {
        public static IList<ValidationResult> Validate(object model)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, result, true);
            return result;
        } 
    }
}