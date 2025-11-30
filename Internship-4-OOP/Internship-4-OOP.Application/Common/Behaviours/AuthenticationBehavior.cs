using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Domain.Common.Exceptions;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Common.Behaviours;

public class AuthenticationBehavior<TRequest, TResponse>(IUserRepository repository)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequireAuthentification
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var user=await repository.AuthenticateAsync(request.Username,request.Password);
        
        if(user==null)
            throw new UnauthenticatedException("Autentifikacija korisnika neuspješna");
        
        if (!user.IsActive)
            throw new UnauthenticatedException("Korisnik nije aktivan");
        
        request.SetAuthentificatedUser(user);
        
        return await next(cancellationToken);
    }
}