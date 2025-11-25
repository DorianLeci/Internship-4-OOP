
namespace Internship_4_OOP.Domain.Common.Model;

public class Result<TValue,TError>
{
    public TValue? Value {get; }
    public TError?  Error {get;}

    public bool IsFailure { get; }

    private Result(TError error)
    {
        IsFailure = true;
        Value = default;
        Error = error;
    }

    private Result(TValue value)
    {
        Value = value;
        Error = default;
        IsFailure = false;
    }

    public static Result<TValue, TError> Failure(TError error) => new(error);
    public static Result<TValue, TError> Success(TValue value) => new(value);
}