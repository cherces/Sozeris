namespace Sozeris.Server.Logic.Common;

public class Result
{
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }

    protected Result(bool success, string errorMessage)
    {
        Success = success;
        ErrorMessage = errorMessage;
    }
    
    public static Result Ok() => new Result(true, string.Empty);
    public static Result Fail(string errorMessage) => new Result(false, errorMessage);
}

public class Result<T> : Result
{
    public T Value { get; }

    private Result(bool success, T value, string errorMessage) : base(success, errorMessage)
    {
        Value = value;
    }

    public static Result<T> Ok(T data) => new Result<T>(true, data, string.Empty);
    public new static Result<T> Fail(string errorMessage) => new Result<T>(false, default!, errorMessage);
}