using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Internship_4_OOP.Application.Common.Behaviours;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest:notnull
{
    private readonly ILogger<TRequest> _logger;
    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
            _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return next();
        }
        catch (ValidationException e)
        {
            _logger.LogWarning("Zahtjev korisnika prekinut: {Name} {@request}", typeof(TRequest).Name, request);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Zahtjev: neobrađena iznimka: {Name} {@request}", typeof(TRequest).Name, request);
            throw;
        }
    }
}