using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Application.Common.Interfaces;

public interface IRequireAuthentification
{
    string Username { get; }
    string Password { get; }
    void SetAuthentificatedUser(User user);
}