namespace Sozeris.Server.Logic.Common;

public class Result
{
    public bool IsSuccess { get; set; }
    public DomainError? Error { get; set; }

    protected Result(bool isSuccess, DomainError? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Ok() => new Result(true);
    public static Result Fail(DomainError error) => new Result(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value = default, DomainError? error = null) 
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Ok(T value) => new Result<T>(true, value);
    public new static Result<T> Fail(DomainError error) => new Result<T>(false, default, error);
}