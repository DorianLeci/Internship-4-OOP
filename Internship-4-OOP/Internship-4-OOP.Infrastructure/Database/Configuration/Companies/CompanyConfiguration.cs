using Internship_4_OOP.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_4_OOP.Infrastructure.Database.Configuration.Companies;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {   
            builder.ToTable("companies",schema:"public");
            builder.HasKey(company => company.Id);
            builder.Property(company => company.Id).HasColumnName("id");
            builder.Property(company=>company.Name).HasColumnName("name");
            builder.Property(company=>company.CreatedAt).HasColumnName("created_at");
            builder.Property(company=>company.UpdatedAt).HasColumnName("updated_at");

        }
    }
