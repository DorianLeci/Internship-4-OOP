namespace Internship_4_OOP.Domain.Common.Validation.ValidationItems;

public static partial class ValidationItems
{
    public static class User
    {
        public static string CodePrefix = nameof(User);

        public static readonly ValidationItem NameMaxLength = new ValidationItem($"{CodePrefix}1",$"Ime ne smije biti duže od {User.NameMaxLength} znakova",
        ValidationSeverity.Error,ValidationType.FormalValidation);
        
        public static readonly 
    }
}