namespace Internship_4_OOP.Domain.Common.Validation;

public class ValidationResult
{
    private List<ValidationItem> _validationItems = [];
    public IReadOnlyList<ValidationItem> ValidationItems => _validationItems;
}