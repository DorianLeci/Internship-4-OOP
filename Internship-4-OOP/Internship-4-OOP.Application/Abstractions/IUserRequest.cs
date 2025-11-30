namespace Internship_4_OOP.Application.Abstractions;

public interface IUserRequest
{
    public string Name{get; init;}
    public string Username{get; init;}
    public string Email{get; init;}
    public string AddressStreet{get; init;}
    public string AddressCity{get; init;}
    public decimal GeoLatitude{get; init;}
    public decimal GeoLongitude{get; init;}
    public string? Website { get; init; }
}