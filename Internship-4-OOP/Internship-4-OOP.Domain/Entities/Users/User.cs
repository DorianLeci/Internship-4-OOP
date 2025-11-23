using System.ComponentModel.DataAnnotations;
using Internship_4_OOP.Domain.Common.Model;

namespace Internship_4_OOP.Domain.Entities.Users;

public class User
{
    public const int NameMaxLength = 100;
    public int Id{get; set;}
    public string Name{get; set;}
    public string Username{get; set;}
    public string Email{get; set;}
    public string AdressStreet{get; set;}
    public string AdressCity{get; set;}
    public decimal GeoLatitude{get; set;}
    public decimal GeoLongitude{get; set;}
    public string? Website;
    public string Password{get; set;}

    public async Task<Result<int>> Create()
    {
        
    }

    public async Task<ValidationResult> CreateOrUpdateValidation()
    {
        if (Name.Length > NameMaxLength)
            //validacija
    }
}