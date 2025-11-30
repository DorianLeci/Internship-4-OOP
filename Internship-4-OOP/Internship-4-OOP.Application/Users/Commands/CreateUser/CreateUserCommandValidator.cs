using FluentValidation;
using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.RuleBuilder;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        UserValidationRules.ApplyRules(this);
    }


}
