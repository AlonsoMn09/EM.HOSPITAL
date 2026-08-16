namespace EM.Hospital.Domain.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Message { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyCollection<string> Errors { get; }
        protected internal Result(bool isSuccess, string? message, IEnumerable<string>? errors = null)
        {
            if (isSuccess && (errors?.Any() == true))
                throw new InvalidOperationException();
            if (!isSuccess && string.IsNullOrEmpty(message) && errors?.Any() != true)
                throw new InvalidOperationException();
            IsSuccess = isSuccess;
            Message = message ?? string.Empty;
            Errors = errors?.ToList() ?? new List<string>();
        }
        public static Result Success(string message = "Process succesfully") => new (true, message);
        public static Result Failure(string message) => new (false, message);
        public static Result Failure(IEnumerable<string> errors) => new(false, "Process failed", errors);
        public static Result<T> Success<T>(T value) => new(value, true, "Process Successfully");
        public static Result<T> Failure<T>(string message) => new(default, false, message);
        public static Result<T> Failure<T>(IEnumerable<string> errors) => new(default!, false, "Process failed", errors);
    }

    public class Result<T> : Result
    {
        public T Value { get; }
        protected internal Result(T value, bool isSuccess, string? message, IEnumerable<string>? errors = null)
            : base(isSuccess, message, errors)
        {
            Value = value;
        }       
    }
}
