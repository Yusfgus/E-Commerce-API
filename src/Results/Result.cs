namespace E_Commerce.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; }

    protected Result()
    {
        IsSuccess = true;
        Errors = [];
    }

    protected Result(List<Error> errors)
    {
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        IsSuccess = false;
    }

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<List<Error>, TResult> onFailure)
    {
        if (IsSuccess)
            return onSuccess();

        return onFailure(Errors);
    }

    public static Result Success => new();

    public static implicit operator Result(Error error)
        => new([error]);

    public static implicit operator Result(List<Error> errors)
        => new(errors);
    
}

public sealed class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base()
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    private Result(List<Error> errors) : base(errors)
    {
        Value = default;
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<List<Error>, TResult> onFailure)
    {
        if (IsSuccess)
            return onSuccess(Value!);

        return onFailure(Errors);
    }

    public static implicit operator Result<T>(T value)
        => new(value);

    public static implicit operator Result<T>(Error error)
        => new([error]);

    public static implicit operator Result<T>(List<Error> errors)
        => new(errors);

}
