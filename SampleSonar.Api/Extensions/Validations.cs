using SampleSonar.Data.Models.Responses;
using System.ComponentModel.DataAnnotations;

namespace SampleSonar.Api.Extensions
{
    public class Validations
    {
        public static GenericResponse IsValid(object value)
        {
            GenericResponse response = new();
            var context = new ValidationContext(value, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            List<ValidationAttribute> m_validationAttributes = new List<ValidationAttribute>();


            response.Code = Validator.TryValidateValue(value, context, validationResults, m_validationAttributes) ? "91" : "96";
            response.Message = validationResults == null ? null : validationResults[0].ErrorMessage;
            response.Success = validationResults == null;
            return response;
        }
    }
}
