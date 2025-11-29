using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Common.Base;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Database.Configuration;

public class ApplicationDbContext : DbContext,IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var insertedEntries = ChangeTracker.Entries()
            .Where(entry => entry.State == EntityState.Added)
            .Select(entry => entry.Entity);
        
        foreach (var insertedEntry in insertedEntries)
            if (insertedEntry is IAuditableEntity auditableEntity)
            {
                auditableEntity.CreatedAt = DateTime.UtcNow;
                auditableEntity.UpdatedAt = DateTime.UtcNow;
            }
        
        var modifiedEntries = ChangeTracker.Entries()
            .Where(entry => entry.State == EntityState.Modified)
            .Select(entry => entry.Entity);
        
        foreach (var modifiedEntry in modifiedEntries) 
            if (modifiedEntry is IAuditableEntity auditableEntity)
            {
                auditableEntity.UpdatedAt = DateTime.UtcNow;
            }

        return await base.SaveChangesAsync(cancellationToken);
    }

    
    
}