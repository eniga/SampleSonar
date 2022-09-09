using Microsoft.AspNetCore.Mvc.ModelBinding;
using SampleSonar.Data.Models.Responses;

namespace SampleSonar.Api.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
            => dictionary.SelectMany(m => m.Value.Errors)
            .Select(m => m.ErrorMessage)
            .ToList();

        public static GenericResponse GetErrorMessage(this ModelStateDictionary dictionary)
            => new GenericResponse() { Code = "91", Message = GetErrorMessages(dictionary).FirstOrDefault(), Success = false };
    }
}
