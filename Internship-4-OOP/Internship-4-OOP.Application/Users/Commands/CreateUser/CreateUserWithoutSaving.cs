using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.Companies.Commands.CreateCompany;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands.CreateUser;

public record CreateUserWithoutSavingCommand(
    string Name,
    string Username,
    string Email,
    string AddressStreet,
    string AddressCity,
    decimal GeoLatitude,
    decimal GeoLongitude,
    string? Website,
    string CompanyName
) : IRequest<Result<int,IDomainError>>

{
    public static CreateUserWithoutSavingCommand FromDto(CreateUserDto dto)
    {
        return new CreateUserWithoutSavingCommand(dto.Name,dto.Username,dto.Email,dto.AddressStreet,dto.AddressCity,dto.GeoLatitude,dto.GeoLongitude,dto.Website,dto.CompanyName);
    }
}
public class CreateUserWithoutSavingCommandHandler(IUserRepository userRepository,IMediator mediator,IUserDbContext dbContext,ICompanyDbContext companyDbContext) : 
    IRequestHandler<CreateUserWithoutSavingCommand, Result<int,IDomainError>>
{

    public async Task<Result<int,IDomainError>> Handle(CreateUserWithoutSavingCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByUsernameAsync(request.Username))
        {
            return Result<int, IDomainError>.Failure(
                DomainError.Conflict("Već postoji korisnik s istim korisničkim imenom."));
        }
        if (await userRepository.ExistsByEmailAsync(request.Email))
        {
            return Result<int, IDomainError>.Failure(DomainError.Conflict("Već postoji korisnik s istim emailom."));
        }

        if (await userRepository.ExistsUserWithinDistanceAsync(request.GeoLatitude, request.GeoLongitude, 3))
        {
            return Result<int, IDomainError>.Failure(DomainError.Conflict("Postoji korisnik unutar 3 kilometra od trenutno unesenog."));
        }

        var companyResult = await mediator.Send(new CreateCompanyCommand(request.CompanyName));
        if (companyResult.IsFailure)
            return companyResult;
        
        var newUser = new User(request.Name,request.Username,request.Email,request.AddressStreet,request.AddressCity,
            request.GeoLatitude,request.GeoLongitude,request.Website,companyResult.Value);
        
        await userRepository.InsertAsync(newUser);
        
        return Result<int,IDomainError>.Success(newUser.Id);

    }
}