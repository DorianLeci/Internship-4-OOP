using FluentValidation;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.DTO.CompanyDto;
using Internship_4_OOP.Domain.Common.Exceptions;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;
using ValidationException = Internship_4_OOP.Domain.Common.Exceptions.ValidationException;

namespace Internship_4_OOP.Application.Common.Behaviours;

public class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (ValidationException e)
        {
            
            var domainError=DomainError.Validation(e.Message,e.Errors.ToList());
            var failureResult = Result<int, IDomainError>.Failure(domainError);
            
            logger.LogError(e, "Zahtjev: neuspješna validacija: {@request}", request);
            
            if (failureResult is TResponse response)
                return response;

            throw new InvalidCastException("Response je pogrešno castan.");

        }
        catch (UnauthenticatedException e)
        {
            var domainError=DomainError.Unathorized(e.Message);
            var failureResult = Result<GetCompanyDto, IDomainError>.Failure(domainError);
            
            logger.LogError(e, "Zahtjev: neuspješna autentifikacija korisnika: {@request}", request);
            
            if (failureResult is TResponse response)
                return response;

            throw new InvalidCastException("Response je pogrešno castan.");

        }
        catch (Exception e)
        {
            var domainError = DomainError.Unexpected(e.Message);
            var failureResult = Result<int, DomainError>.Failure(domainError);
            
            logger.LogError(e, "Zahtjev: neobrađena iznimka: {@request}", request);

            if (failureResult is TResponse response)
                return response;
            
            throw new InvalidCastException("Response je pogrešno castan.");

        }
        
    }
}