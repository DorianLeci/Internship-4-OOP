using Internship_4_OOP.Application.DTO;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Internship_4_OOP.Application.Common.Behaviours;

public class LoggingBehavior<TRequest>(ILogger<TRequest> logger) : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var nameOfRequest=typeof(TRequest).Name;
        
        logger.LogInformation("Request info: {@_item}", request);
        
        return Task.CompletedTask;
    }
}

