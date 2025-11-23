using System.ComponentModel.DataAnnotations;

namespace Internship_4_OOP.Domain.Common.Model;

public class Result<TValue>
{
    public TValue Value {get; private set;}
    public ValidationResult ValidationResult {get; private set;}

    public Result(TValue value,ValidationResult validationResult)
    {
        Value=value;
        ValidationResult=validationResult;
    }
}