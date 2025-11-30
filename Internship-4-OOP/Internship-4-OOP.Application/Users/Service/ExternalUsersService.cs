using System.Net.Http.Json;
using Internship_4_OOP.Application.DTO;

namespace Internship_4_OOP.Application.Users.Service;

public class ExternalUsersService(HttpClient httpClient)
{
    public async Task<List<ExternalUsersDto>?> ListExternalUsersAsync()
    {
        var response=await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
        if (!response.IsSuccessStatusCode)
            return null;
        
        return await response.Content.ReadFromJsonAsync<List<ExternalUsersDto>>();
    }
}