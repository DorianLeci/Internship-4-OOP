using Internship_4_OOP.Domain.Common.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Internship_4_OOP.Application.Events;

public class DomainEventLogger<TEvent,TItem>(ILogger<TEvent> logger) : INotificationHandler<TEvent> where TEvent:BaseEvent<TItem>
{
    public Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
       logger.LogInformation("Domain event: {EventName} {@DomainEvent}",notification.GetType().Name,notification);
       return Task.CompletedTask;
    }
}