using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Application.Users;

public static class UserMapper
{
    public static GetUserDto GetDtoFromUser(User user)
    {
        return new GetUserDto
        {
            Name=user.Name,
            Username = user.Username,
            Email = user.Email,
            AddressStreet = user.AddressStreet,
            AddressCity = user.AddressCity,
            GeoLatitude = user.GeoLatitude,
            GeoLongitude = user.GeoLongitude,
            Website = user.Website,
        };
    }
}