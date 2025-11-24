using System.Runtime.CompilerServices;
using Internship_4_OOP.Domain.Common.Validation;

namespace Internship_4_OOP.Application.Common.Validation;

public class ValidationResultItem(
    string message,
    string code,
    ValidationSeverity validationSeverity,
    ValidationType validationType)
{
    public ValidationSeverity ValidationSeverity { get; init; } = validationSeverity;
    public ValidationType ValidationType { get; init; } = validationType;
    public string Message { get; init; } = message;
    public string Code { get; init; } = code;

    public static ValidationResultItem FormValidationItem(ValidationItem validationItem)
    {
        return new ValidationResultItem(validationItem.Message, validationItem.Code, validationItem.ValidationSeverity, validationItem.ValidationType);
    }
}