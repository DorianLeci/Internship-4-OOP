
using Internship_4_OOP.Domain.Common.Validation;

namespace Internship_4_OOP.Domain.Common.Model;

public class Result<TValue>(TValue value, ValidationResult validationResult)
{
    public TValue Value {get; private set;} = value;
    public ValidationResult ValidationResult {get; private set;} = validationResult;
}