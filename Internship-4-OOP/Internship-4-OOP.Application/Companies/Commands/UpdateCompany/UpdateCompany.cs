using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Common.Events.Company;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.Company;
using MediatR;

namespace Internship_4_OOP.Application.Companies.Commands.UpdateCompany;

public record UpdateCompanyCommand(int CompanyId, string CompanyName):IRequest<Result<int,IDomainError>>;

public class UpdateCompanyCommandHandler(ICompanyRepository companyRepository,IMediator mediator,ICompanyDbContext dbContext) : IRequestHandler<UpdateCompanyCommand,Result<int,IDomainError>>
{

    public async Task<Result<int,IDomainError>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company=await companyRepository.GetByIdAsyncWithCore(request.CompanyId);
        
        if(company==null)
            return Result<int,IDomainError>.Failure(DomainError.NotFound("Ne postoji kompanija s traženim id-om"));
        
        
        if (await companyRepository.ExistsByNameAsync(request.CompanyName,excludeId:request.CompanyId))
        {
            return Result<int, IDomainError>.Failure(
                DomainError.Conflict("Već postoji kompanija s istim imenom."));
        }
        
        company.Name = request.CompanyName;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        company.AddDomainEvent(new CompanyCreatedEvent(2,"CompanyUpdatedEvent",company.Id,DateTimeOffset.Now,company));
        await mediator.Publish(company.DomainEvents.Last());
        
        return Result<int,IDomainError>.Success(company.Id);

    }
    
}