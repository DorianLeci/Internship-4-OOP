using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Database.Configuration.Companies;

public class CompanyDbContext:DbContext,IApplicationDbContext
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
    {
    }
    public DbSet<Company> Companies { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyConfiguration).Assembly);
    }
}