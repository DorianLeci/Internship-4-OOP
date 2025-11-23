
namespace Internship_4_OOP.Domain.Common.Validation;

public class ValidationResult
{
    private readonly List<ValidationItem> _validationItems = [];
    public IReadOnlyList<ValidationItem> ValidationItems => _validationItems;
    
    public bool HasErrors => _validationItems.Any(validationItem=>validationItem.ValidationSeverity==ValidationSeverity.Error);
    public bool HasInfo => _validationItems.Any(validationItem=>validationItem.ValidationSeverity==ValidationSeverity.Info);
    public bool HasWarning => _validationItems.Any(validationItem=>validationItem.ValidationSeverity==ValidationSeverity.Warning);
    
    public void AddValidationItem(ValidationItem validationItem)
    {
        _validationItems.Add(validationItem);
    }
}