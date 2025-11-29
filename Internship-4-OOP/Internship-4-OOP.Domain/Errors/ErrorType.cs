using System.Text.Json.Serialization;

namespace Internship_4_OOP.Domain.Errors;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorType
{
    Conflict,
    Validation,
    Unexecpected,
    NotFound,
    BadRequest
}