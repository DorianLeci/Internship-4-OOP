using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Companies.Commands.GetCompany;


public record GetCompanyByIdQuery(int Id, string Username, string Password) : IQuery<GetCompanyDto>,IRequireAuthentification
{
    public User AuthenticatedUser { get;private set; }

    public static GetCompanyByIdQuery FromDto(int id,string username, string password)
    {
        return new GetCompanyByIdQuery(id,username, password);
    }

    public void SetAuthentificatedUser(User user)
    {
        AuthenticatedUser = user;
    }
}

public class GetCompanyByIdQueryHandler(ICompanyRepository companyRepository,IUserRepository userRepository) : IQueryHandler<GetCompanyByIdQuery, GetCompanyDto>
{
    public async Task<Result<GetCompanyDto, IDomainError>> Handle(GetCompanyByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = request.AuthenticatedUser;

        var company = await companyRepository.GetByIdAsync(request.Id);
        if (company == null)
            return Result<GetCompanyDto, IDomainError>.Failure(DomainError.NotFound("Kompanija s unesenim id-om ne postoji"));

        Console.WriteLine(user.CompanyId);
        if (company.Id!=user.CompanyId)
            return Result<GetCompanyDto, IDomainError>.Failure(DomainError.Unathorized("Kompanija nije povezana s korisnikom"));
        
        var companyDto = CompanyMapper.GetDtoFromCompany(company);
            
        return  Result<GetCompanyDto, IDomainError>.Success(companyDto);
    }


}