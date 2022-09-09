namespace SampleSonar.Data.Models.Responses
{
    public class GenericResponse
    {
        public string Code { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
        public bool Success { get; set; } = false;
    }

    public class Response<T> : GenericResponse
    {
        public T? Data { get; set; }
    }
}
