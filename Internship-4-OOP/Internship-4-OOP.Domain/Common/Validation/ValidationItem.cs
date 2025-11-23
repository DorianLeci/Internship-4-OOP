namespace Internship_4_OOP.Domain.Common.Validation;

public class ValidationItem
{
    public ValidationSeverity ValidationSeverity { get; set; }
    public ValidationType ValidationType { get; init; }
    public string Code { get; init; }
    public string Message { get; init; }
}