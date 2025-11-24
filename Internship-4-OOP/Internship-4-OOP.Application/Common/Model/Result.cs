using Internship_4_OOP.Application.Common.Validation;
using Internship_4_OOP.Domain.Common.Validation;

namespace Internship_4_OOP.Application.Common.Model;

public class Result<TValue> where TValue:class
{
    private List<ValidationResultItem> _infos = [];
    private List<ValidationResultItem> _warnings = [];
    private List<ValidationResultItem> _errors = [];
    
    public TValue? Value { get; set; }
    public Guid RequestId { get; set; }
    
    public bool IsAuthorized { get; set; }

    public IReadOnlyList<ValidationResultItem> Infos
    {
        get => _infos.AsReadOnly();
        init=>_infos.AddRange(value);
    }
    public IReadOnlyList<ValidationResultItem> Warnings
    {
        get => _warnings.AsReadOnly();
        init=>_warnings.AddRange(value);
    }
    public IReadOnlyList<ValidationResultItem> Errors
    {
        get => _errors.AsReadOnly();
        init=>_errors.AddRange(value);
    }
    
    public bool HasErrors=>Errors.Any(validationResult=>validationResult.ValidationSeverity==Domain.Common.Validation.ValidationSeverity.Error);

    public void SetResult(TValue value)
    {
        Value = value;
    }
    public void SetUnauthorizedResult()
    {
        Value = null;
        IsAuthorized = false;
    }

    public void SetValidationResult(ValidationResult validationResult)
    {
        _errors.AddRange(validationResult.ValidationItems.Where(validationItem=>validationItem.ValidationSeverity==ValidationSeverity.Error).Select(
            validationItem=>ValidationResultItem.FormValidationItem(validationItem)));
        
        _infos.AddRange(validationResult.ValidationItems.Where(validationItem=>validationItem.ValidationSeverity==ValidationSeverity.Info).Select(
            validationItem=>ValidationResultItem.FormValidationItem(validationItem)));
        
        _warnings.AddRange(validationResult.ValidationItems.Where(validationItem=>validationItem.ValidationSeverity==ValidationSeverity.Warning).Select(
            validationItem=>ValidationResultItem.FormValidationItem(validationItem))
    }
}