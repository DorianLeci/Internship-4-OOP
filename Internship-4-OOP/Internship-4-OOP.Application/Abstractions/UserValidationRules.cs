using FluentValidation;
using Internship_4_OOP.Application.RuleBuilder;

namespace Internship_4_OOP.Application.Abstractions;

public static class UserValidationRules
{
    public static void ApplyRules<T>(AbstractValidator<T> validator)
    where T:IUserRequest
    {
            
        const string nameReq = "Ime korisnika";
        const string usernameReq = "Korisničko ime ";
        const string emailVal = "Email";
        const string adressStreetVal = "Ulična adresa";
        const string adressCityVal = "Adresa grada";
        const string geoLatVal = "Geografska širina";
        const string geoLongVal = "Geografska dužina";
        const string webSiteVal = "Web stranica";
        const string companyVal = "Id kompanije";

        
        validator.RuleFor(request => request.Name).Required(nameReq).DependentRules(()=>validator.RuleFor(request=>request.Name).MaxLength(nameReq, 100));

        validator.RuleFor(request => request.Username).Required(usernameReq)
            .DependentRules(() => validator.RuleFor(request => request.Username).MaxLength(usernameReq, 30));

        validator.RuleFor(request => request.Email).Required(emailVal).DependentRules(() =>
        {
            validator.RuleFor(request => request.Email).MaxLength(emailVal, 150);
            validator.RuleFor(request => request.Email).EmailValidator(emailVal);
        });


        validator.RuleFor(request => request.AddressStreet).Required(adressStreetVal)
            .DependentRules(() => validator.RuleFor(request => request.AddressStreet).MaxLength(adressStreetVal, 150));
        
        validator.RuleFor(request=>request.AddressCity).Required(adressCityVal)
            .DependentRules(() => validator.RuleFor(request => request.AddressCity).MaxLength(adressCityVal,100));

        validator.RuleFor(request => request.GeoLatitude).GeoCoordValidator(geoLatVal, -90m, 90m);

        validator.RuleFor(request => request.GeoLongitude).GeoCoordValidator(geoLongVal, -180m, 180m);

        validator.RuleFor(request => request.Website).MaxLengthForWebsite(webSiteVal,100).WebsiteUrlValidator();
    }
}