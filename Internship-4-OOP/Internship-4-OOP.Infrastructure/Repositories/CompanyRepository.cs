using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Infrastructure.Database.Configuration.Companies;
using Internship_4_OOP.Infrastructure.Manager;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class CompanyRepository(CompanyDbContext context,IDapperManager<Company> dapperManager):Repository<Company,int>(context,dapperManager),ICompanyRepository
{
    public async Task<Company?> GetByIdAsync(int id)
    {
        string sql = "SELECT* FROM Companies WHERE id = @Id";
        
        return await DapperManager.QuerySingleAsync(sql,new { Id = id });        
    }

    public async Task<Company?> GetByIdAsyncWithCore(int id)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name,int? excludeId=null)
    {
        return await DbSet.AnyAsync(company=>company.Name==name && (!excludeId.HasValue || company.Id!=excludeId));        
    }
    public async Task<bool> CompanyNameExistsAsync(string companyName)
    {
        return await context.Companies.AnyAsync(c => c.Name == companyName);
    }
}