namespace MiRenta.Application.Common.Models
{
    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Conflict
    }

    public class Result<T>
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }

        public static Result<T> Ok(T data) => new() { Success = true, Data = data };
        public static Result<T> Fail(string error, ErrorType errorType = ErrorType.Validation)
            => new() { Success = false, Error = error, ErrorType = errorType };
    }

    public class Result
    {
        public bool Success { get; set; }
        public ErrorType ErrorType { get; set; }
        public string? Error { get; set; }

        public static Result Ok() => new() { Success = true };

        public static Result Fail(string error, ErrorType errorType = ErrorType.Validation)
            => new() { Success = false, Error = error, ErrorType = errorType };
    }
}
