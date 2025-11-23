using System.Text.Json.Serialization;
namespace Internship_4_OOP.Domain.Common.Validation;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ValidationType
{
    FormalValidation,
    BusinessRule,
    SystemError
}