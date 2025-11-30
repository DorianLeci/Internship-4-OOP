using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Domain.Entities.Company;

namespace Internship_4_OOP.Application.Companies;

public static class CompanyMapper
{
    public static GetCompanyDto GetDtoFromCompany(Company company)
    {
        return new GetCompanyDto
        {
            Id= company.Id,
            Name=company.Name,
        };
    }
}