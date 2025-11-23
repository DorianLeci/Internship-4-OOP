namespace Internship_4_OOP.Domain.Common.Validation;

public class ValidationItem(
    string code,
    string message,
    ValidationSeverity validationSeverity,
    ValidationType validationType)
{
    public ValidationSeverity ValidationSeverity { get; set; } = validationSeverity;
    public ValidationType ValidationType { get; init; } = validationType;
    public string Code { get; init; } = code;
    public string Message { get; init; } = message;
}