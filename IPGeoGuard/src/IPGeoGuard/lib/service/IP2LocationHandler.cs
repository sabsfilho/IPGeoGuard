
namespace IPGeoGuard.lib.service;
internal class IP2LocationHandler
{
    internal static (string country, string city) RequestGeolocation(string ip)
    {
        string country = "Brazil "+ip;
        string city = "Rio";

        return (country, city);
    }
}