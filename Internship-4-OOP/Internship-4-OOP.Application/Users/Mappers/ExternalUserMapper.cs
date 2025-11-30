using Internship_4_OOP.Application.Common;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.DTO.UserDto;

namespace Internship_4_OOP.Application.Users.Mappers;

public static class ExternalUserMapper
{
    public static CreateUserDto GetDtoFromExternal(ExternalUsersDto user)
    {
        
        return new CreateUserDto()
        {
            Name=user.Name,
            Username = user.Username,
            Email = user.Email,
            AddressStreet = user.Address.AddressStreet,
            AddressCity = user.Address.AddressCity,
            GeoLatitude = user.Address.Geo.GeoLatitude,
            GeoLongitude = user.Address.Geo.GeoLongitude,
            Website = UrlParser.FormatWebsite(user.Website),
            CompanyName =  user.Company.CompanyName
        };
    }
}