using Internship_4_OOP.Domain.Common.Base;
using Internship_4_OOP.Domain.Common.Validation.ValidationItems;
using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Domain.Events;

public class UserCreatedEvent(User user) : BaseEvent
{
    public User User { get; } = user;
}