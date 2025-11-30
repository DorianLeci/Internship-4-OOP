using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.Common;
using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.Companies.Commands.CreateCompany;
using Internship_4_OOP.Application.Companies.Commands.UpdateCompany;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.DTO.UserDto;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Common.Events.User;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    int Id,
    string Name,
    string Username,
    string Email,
    string AddressStreet,
    string AddressCity,
    decimal GeoLatitude,
    decimal GeoLongitude,
    string? Website) : IRequest<Result<int, IDomainError>>,IUserRequest
{
    public static UpdateUserCommand FromDto(int id,UpdateUserDto dto)
    {
        return new UpdateUserCommand(id,dto.Name,dto.Username,dto.Email,dto.AddressStreet,dto.AddressCity,dto.GeoLatitude,dto.GeoLongitude,UrlParser.FormatWebsite(dto.Website));
    }
}

public class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IMediator mediator,
    IUserDbContext dbContext) : IRequestHandler<UpdateUserCommand, Result<int, IDomainError>>
{
    public async Task<Result<int, IDomainError>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user=await userRepository.GetByIdAsyncWithCore(request.Id);
        
        if (user==null)
            return Result<int,IDomainError>.Failure(DomainError.NotFound("Korisnik s unesenim id-om nije pronađen"));
        
        if (!IsAnythingChanged.UserChanged(user, request))
        { 
            return Result<int, IDomainError>.Failure(DomainError.Conflict("Nije napravljena nijedna promjena."));
        }
        
        if (await userRepository.ExistsByUsernameAsync(request.Username,excludeId:request.Id))
        {
            return Result<int, IDomainError>.Failure(
                DomainError.Conflict("Već postoji korisnik s istim korisničkim imenom."));
        }

        if (await userRepository.ExistsByEmailAsync(request.Email,excludeId:request.Id))
        {
            return Result<int, IDomainError>.Failure(DomainError.Conflict("Već postoji korisnik s istim emailom."));
        }

        if (await userRepository.ExistsUserWithinDistanceAsync(request.GeoLatitude, request.GeoLongitude, 3,excludeId:request.Id))
        {
            return Result<int, IDomainError>.Failure(
                DomainError.Conflict("Postoji korisnik unutar 3 kilometra od trenutno unesenog."));
        }

        user.Name = request.Name;
        user.Username = request.Username;
        user.Email = request.Email;
        user.AddressStreet = request.AddressStreet;
        user.AddressCity = request.AddressCity;
        user.GeoLatitude = request.GeoLatitude;
        user.GeoLongitude = request.GeoLongitude;
        user.Website = request.Website;
        
        await dbContext.SaveChangesAsync(cancellationToken);

        user.AddDomainEvent(new UserCreatedEvent(2, "UserUpdatedEvent", user.Id, DateTimeOffset.Now, user));

        await mediator.Publish(user.DomainEvents.Last());

        return Result<int, IDomainError>.Success(user.Id);

    }
    
}



