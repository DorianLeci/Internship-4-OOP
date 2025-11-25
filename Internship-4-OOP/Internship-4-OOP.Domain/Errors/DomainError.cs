namespace Internship_4_OOP.Domain.Errors;

public record DomainError
{
    public string? ErrorMessage { get; init; }
    public ErrorType ErrorType { get; init; }
    public List<string>? Errors { get; init; }
    private DomainError(string? message, ErrorType errorType, List<string>? errors = null)
    {
        ErrorMessage = message;
        ErrorType = errorType;
        Errors = errors;
    }
    public static DomainError Conflict(string ? message)=>
        new(message ?? "Dogodio se konflikt s podatcima u bazi podataka.", ErrorType.Conflict);
}