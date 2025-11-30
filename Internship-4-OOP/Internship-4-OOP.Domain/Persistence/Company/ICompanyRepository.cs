using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Persistence.Common;

namespace Internship_4_OOP.Domain.Persistence.Company;

public interface ICompanyRepository:IRepository<Entities.Company.Company,int>
{
    Task<Entities.Company.Company?>GetByIdAsync(int id);
    Task<Entities.Company.Company?> GetByIdAsyncWithCore(int id);
    
    Task<bool>ExistsByNameAsync(string name,int? excludeId=null);
    Task<bool> CompanyNameExistsAsync(string companyName);
}