using FluentValidation;
using FluentValidation.Validators;

namespace Internship_4_OOP.Application.Users.Commands;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(request => request.Name).MaximumLength(100).WithMessage("Ime korisnika ne smije biti duže od 50 znakova");
        RuleFor(request => request.Name).NotEmpty().WithMessage("Ime korisnika ne smije biti prazno.");
    }
}