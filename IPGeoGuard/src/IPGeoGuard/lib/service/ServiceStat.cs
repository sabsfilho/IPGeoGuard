


namespace IPGeoGuard.lib.service;

public class ServiceStat
{
    public Dictionary<string, int>? IPList { get; set; }
    public Dictionary<string, CountryStatItem>? CountryList { get; set; }

    internal void IncrementHit(string ip, IPStatInfo info)
    {
        IncrementIP(ip);
        IncrementCountryList(info);
    }

    private void IncrementCountryList(IPStatInfo info)
    {
        string country = info.Country;
        if (CountryList == null) CountryList = new Dictionary<string, CountryStatItem>();
        CountryStatItem statItem;
        if (!CountryList.TryGetValue(country, out statItem!))
        {
            statItem = new CountryStatItem()
            {
                Name = country
            };
            CountryList.Add(country, statItem);
        }
        statItem.Hits++;

        IncrementCityList(info.City, statItem);
    }
    private static void IncrementCityList(string city, CountryStatItem countryStatItem)
    {
        var cityList = countryStatItem.CityList;
        if (cityList == null) 
        {
            cityList = new Dictionary<string, CityStatItem>();
            countryStatItem.CityList = cityList;
        }
        CityStatItem statItem;
        if (!cityList.TryGetValue(city, out statItem!))
        {
            statItem = new CityStatItem()
            {
                Name = city
            };
            cityList.Add(city, statItem);
        }
        statItem.Hits++;
    }

    private void IncrementIP(string ip)
    {
        if (IPList == null) IPList = new Dictionary<string, int>();
        int q = 0;
        if (!IPList.TryGetValue(ip, out q))
        {
            IPList.Add(ip, 1);
        }
        else
        {
            IPList[ip] = q + 1;
        }
    }
}
