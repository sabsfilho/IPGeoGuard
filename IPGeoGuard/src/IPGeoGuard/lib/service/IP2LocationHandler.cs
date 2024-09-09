using System.Text.Json;

namespace IPGeoGuard.lib.service;
internal class IP2LocationHandler
{   
    //private const string IP2LOCATION_API_URL = "https://api.ip2location.io/?key=YOUR_API_KEY&format=json"; 
    //without API_KEY = 500 queries per day
    private const string IP2LOCATION_API_URL = "https://api.ip2location.io/?format=json";
    internal static (string country, string city) RequestGeolocation(string ip)
    {
        string uri = GetIP2LocationAPIAddress(ip);
        var httpClient = new HttpClient();
        var tsk = httpClient.GetStringAsync(uri);
        tsk.Wait();
        string json = tsk.Result;
        
        var x = JsonSerializer.Deserialize<IP2LocationResponse>(json)!;

        return (
            x.country_code ?? "-", 
            x.city_name ?? "-"
        );
    }
    private static string GetIP2LocationAPIAddress(string ip)
    {
        return $"{IP2LOCATION_API_URL}&ip={ip}";
    }
}