namespace ReservaSala.Application;

public struct Result<TValue, TError>
{
    public TValue? Value { get; }
    public TError? Error { get; }
    public bool Success { get; }

    private Result(TValue? value = default, TError? error = default, bool success = false)
    {
        Value = value;
        Error = error;
        Success = success;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value, success: true);
    public static Result<TValue, TError> SetError(TError error) => new(error: error);
}
