using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Database.Configuration;

public static class GlobalLowercaseMapping
{
    public static void Mapping(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.Name.ToLower());
            }
        }        
    }
}