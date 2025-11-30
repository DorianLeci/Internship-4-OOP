using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.DTO.UserDto;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands.ImportExternal;

public record ImportExternalUsersCommand(List <CreateUserDto> Users): IRequest<Result<List<int>, IDomainError>>

{
    public static ImportExternalUsersCommand FromExternalDto(List <CreateUserDto> users)
    {
        return new ImportExternalUsersCommand(users);
    }
}
public class ImportExternalUsersCommandHandler(IUserUnitOfWork userUnitOfWork,IMediator mediator) : IRequestHandler<ImportExternalUsersCommand, Result<List<int>, IDomainError>>
{
    public async Task<Result<List<int>,IDomainError>> Handle(ImportExternalUsersCommand request, CancellationToken cancellationToken)
    {
        if (request.Users.Count == 0)
            return Result<List<int>, IDomainError>.Failure(DomainError.ExternalServiceError("Nema podataka za učitati"));
        
        var idList=new List<int>();
        
        await userUnitOfWork.BeginTransactionAsync();

        foreach (var usr in request.Users)
        {
            var createUserCommand = CreateUserWithoutSavingCommand.FromDto(usr);
            var result = await mediator.Send(createUserCommand);

            if (result.IsFailure)
            {
                await userUnitOfWork.RollbackAsync();
                return Result<List<int>, IDomainError>.Failure(result.Error);
            }
            
            idList.Add(result.Value);
        }
        await userUnitOfWork.CommitAsync();
        
        return Result<List<int>, IDomainError>.Success(idList);
    }
}