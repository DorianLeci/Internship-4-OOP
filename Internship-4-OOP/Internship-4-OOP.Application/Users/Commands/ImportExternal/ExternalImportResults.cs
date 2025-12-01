using Internship_4_OOP.Domain.Errors;

namespace Internship_4_OOP.Application.Users.Commands.ImportExternal;

public class ExternalImportResults
{
    public List<int> SuccessfulIds { get; set; } = new List<int>();
    public List<IDomainError> Errors { get; set; }=new List<IDomainError>();
}