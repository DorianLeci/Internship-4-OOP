using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Application.Users.Commands.UpdateUser;

public static class IsAnythingChanged
{
    public static bool UserChanged(User user,UpdateUserCommand request)
    {
        var noChanges =
            user.Name == request.Name &&
            user.Username == request.Username &&
            user.Email == request.Email &&
            user.AddressStreet == request.AddressStreet &&
            user.AddressCity == request.AddressCity &&
            user.GeoLatitude == request.GeoLatitude &&
            user.GeoLongitude == request.GeoLongitude &&
            user.Website == request.Website;

        return !noChanges;

    }    
}