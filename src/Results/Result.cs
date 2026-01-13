namespace E_Commerce.Results;

public readonly record struct Success;
public readonly record struct Created;
public readonly record struct Deleted;
public readonly record struct Updated;

public static class Result
{
    public static Success Success => default;
    public static Created Created => default;
    public static Deleted Deleted => default;
    public static Updated Updated => default;
}

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; }
    public Error? TopError => Errors.Count > 0 ? Errors[0] : null;
    public T? Value { get; }


    private Result(T value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
        IsSuccess = true;
        Errors = [];
    }

    private Result(List<Error> errors)
    {
        Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        IsSuccess = false;
        Value = default;
    }

    public TResult Match<TResult>(Func<T, TResult> onSuccess,
                                  Func<List<Error>, TResult> onFailure)
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
