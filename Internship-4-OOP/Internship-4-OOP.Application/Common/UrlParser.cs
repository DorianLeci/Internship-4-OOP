namespace Internship_4_OOP.Application.Common;

public static class UrlParser
{
    public static string? FormatWebsite(string? website)
    {
        if (!string.IsNullOrEmpty(website) && !website.StartsWith("http://") && !website.StartsWith("https://"))
            return "https://" + website;
        
        return website;
    }   
}
