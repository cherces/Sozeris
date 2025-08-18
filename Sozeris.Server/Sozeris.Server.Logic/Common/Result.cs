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
    public T Data { get; }

    public Result(bool success, T data, string errorMessage) : base(success, errorMessage)
    {
        Data = data;
    }

    public static Result<T> Ok(T data) => new Result<T>(true, data, string.Empty);
    public static new Result<T> Fail(string errorMessage) => new Result<T>(false, default!, errorMessage);
}