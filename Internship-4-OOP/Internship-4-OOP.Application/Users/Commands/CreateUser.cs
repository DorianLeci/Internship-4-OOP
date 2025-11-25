using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Events;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands;
public record CreateUserCommand: IRequest<Result<int,DomainError>>
{
    public int Id{get; set;}
    public string Name{get; set;}
    public string Username{get; set;}
    public string Email{get; set;}
    public string AddressStreet{get; set;}
    public string AddressCity{get; set;}
    public decimal GeoLatitude{get; set;}
    public decimal GeoLongitude{get; set;}
    public string? Website{get; set;}
    private string _password = Guid.NewGuid().ToString();
    public bool IsActive = true;
}

public class CreateUserCommandHandler(IApplicationDbContext context,IUserRepository userRepository) : IRequestHandler<CreateUserCommand,Result<int,DomainError>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IApplicationDbContext _context = context;

    public async Task<Result<int,DomainError>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            return Result<int, DomainError>.Failure(DomainError.Conflict("Već postoji korisnik s istim emailom."));
        }
        var newUser = new User(request.Id,request.Name,request.Username,request.Email,request.AddressStreet,request.AddressCity
        ,request.GeoLatitude,request.GeoLongitude,request.Website);

        newUser.AddDomainEvent(new UserCreatedEvent(newUser));
        
        _context.Users.Add(newUser);
        var id=await _context.SaveChangesAsync(cancellationToken);

        return Result<int,DomainError>.Success(id);

    }
}

