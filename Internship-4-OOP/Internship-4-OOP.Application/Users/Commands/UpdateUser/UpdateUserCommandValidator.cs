using FluentValidation;
using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Persistence.User;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator:AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        UserValidationRules.ApplyRules(this);
        
    }
}