using System.Text.Json.Serialization;

namespace Internship_4_OOP.Application.DTO;

public class ExternalUsersDto
{
    public int Id { get; set; }
    public string Name{get; set;}
    public string Username{get; set;}
    public string Email{get; set;}
    public ExternalAddressDto Address{get; set;}
    
    public string? Phone { get; set; }
    public string? Website { get; set; }
    public ExternalCompanyDto Company{get; set;}
    
}

public class ExternalAddressDto
{
    [JsonPropertyName("street")]
    public string AddressStreet{get; set;}
    
    [JsonPropertyName("suite")]
    public string AddressSuite{get; set;}
    
    [JsonPropertyName("city")]
    public string AddressCity{get; set;}
    
    
    [JsonPropertyName("zipcode")]
    
    public string ZipCode{get; set;}
    
    public ExternalGeoDto Geo{get; set;}
}

public class ExternalGeoDto
{
    [JsonPropertyName("lat")]
    public decimal GeoLatitude{get; set;}
    
    [JsonPropertyName("lng")]
    public decimal GeoLongitude{get; set;}    
}

public class ExternalCompanyDto
{
    [JsonPropertyName("name")]
    public string CompanyName{get; set;}
    public string catchPhrase{get; set;}
    public string Bs{get; set;}
}