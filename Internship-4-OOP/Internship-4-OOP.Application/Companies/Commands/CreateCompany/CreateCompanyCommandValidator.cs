using FluentValidation;
using Internship_4_OOP.Application.RuleBuilder;
using Internship_4_OOP.Domain.Persistence.Company;

namespace Internship_4_OOP.Application.Companies.Commands.CreateCompany;


public class CreateCompanyCommandValidator: AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator(ICompanyRepository repository)
    {
        const string nameReq = "Ime kompanije";

        RuleFor(request => request.Name).Required(nameReq).DependentRules(()=>RuleFor(request=>request.Name).
            MaxLength(nameReq, 150));
        
    }

        
}
