using System.Net.Http.Json;
using System.Text.Json;
using Internship_4_OOP.Application.DTO;
using Microsoft.Extensions.Caching.Distributed;

namespace Internship_4_OOP.Application.Users.Service;

public class ExternalUsersService(HttpClient httpClient,IDistributedCache cache)
{
    public async Task<List<ExternalUsersDto>?> ListExternalUsersAsync()
    {
        const string cacheKey = "ExternalUsers";
        
        var cachedData=await cache.GetStringAsync(cacheKey);
        
        if(!string.IsNullOrEmpty(cachedData))
            return JsonSerializer.Deserialize<List<ExternalUsersDto>>(cachedData);
        
        var response=await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
        if (!response.IsSuccessStatusCode)
            return null;
        
        var users= await response.Content.ReadFromJsonAsync<List<ExternalUsersDto>>();

        if (users == null) 
            return users;
        
        var serialized=JsonSerializer.Serialize(users);
        await cache.SetStringAsync(cacheKey, serialized,
            new DistributedCacheEntryOptions
                { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) }
        );

        return users;
    }
    
}