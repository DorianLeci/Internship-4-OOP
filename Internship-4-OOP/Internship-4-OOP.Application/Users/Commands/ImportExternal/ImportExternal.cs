using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.DTO.UserDto;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands.ImportExternal;

public record ImportExternalUsersCommand(List <CreateUserDto> Users): IRequest<Result<ExternalImportResults, IDomainError>>

{
    public static ImportExternalUsersCommand FromExternalDto(List <CreateUserDto> users)
    {
        return new ImportExternalUsersCommand(users);
    }
}
public class ImportExternalUsersCommandHandler(IUserRepository repository,IMediator mediator) : IRequestHandler<ImportExternalUsersCommand, Result<ExternalImportResults, IDomainError>>
{
    public async Task<Result<ExternalImportResults,IDomainError>> Handle(ImportExternalUsersCommand request, CancellationToken cancellationToken)
    {
        if (request.Users.Count == 0)
            return Result<ExternalImportResults, IDomainError>.Failure(DomainError.ExternalServiceError("Nema podataka za učitati"));
        
        var results=new ExternalImportResults();

        foreach (var usr in request.Users)
        {
            var createUserCommand = CreateUserCommand.FromDto(usr);
            var result = await mediator.Send(createUserCommand);
            
            switch (!result.IsFailure)
            {
                case true:
                    results.SuccessfulIds.Add(result.Value);
                    break;
                case false:
                    results.Errors.Add(result.Error!);
                    break;
            }
            
        }

        if (results.SuccessfulIds.Count != 0) 
            return Result<ExternalImportResults, IDomainError>.Success(results);
        
        var errosToString = results.Errors.Select(err => err.ToString()).ToList();
        return Result<ExternalImportResults, IDomainError>.Failure(DomainError.Validation("Niti jedan korisnik iz vanjskog api-a nije uspješno validiran", errosToString!));
    }
}