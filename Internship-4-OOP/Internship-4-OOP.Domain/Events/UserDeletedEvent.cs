using Internship_4_OOP.Domain.Common.Base;
using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Domain.Events;

public class UserDeletedEvent(User user) : BaseEvent
{
    public User User { get; } = user;
}