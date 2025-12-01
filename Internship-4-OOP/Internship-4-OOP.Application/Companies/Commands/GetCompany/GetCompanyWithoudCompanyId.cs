using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.DTO.CompanyDto;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Companies.Commands.GetCompany;

public record GetCompanyWithoutCompanyIdQuery(string Username, string Password) : IQuery<GetCompanyDto>,IRequireAuthentification
{
    public User AuthenticatedUser { get;private set; }
    public void SetAuthentificatedUser(User user)
    {
        AuthenticatedUser = user;
    }
    
    public static GetCompanyWithoutCompanyIdQuery FromQuery(string username, string password)
    {
        return new GetCompanyWithoutCompanyIdQuery(username, password);
    }
}

public class GetCompanyWithoutCompanyIdHandler(ICompanyRepository companyRepository,IUserRepository userRepository) : IQueryHandler<GetCompanyWithoutCompanyIdQuery, GetCompanyDto>
{
    public async Task<Result<GetCompanyDto, IDomainError>> Handle(GetCompanyWithoutCompanyIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = request.AuthenticatedUser;
        
        var company=await companyRepository.GetByIdAsync(user.CompanyId);
        if(company==null)
            return Result<GetCompanyDto,IDomainError>.Failure(DomainError.NotFound("Kompanija s kojom je povezan korisnik ne postoji"));
        
        
        var companyDto = CompanyMapper.GetDtoFromCompany(company);
        return  Result<GetCompanyDto, IDomainError>.Success(companyDto);
    }


}