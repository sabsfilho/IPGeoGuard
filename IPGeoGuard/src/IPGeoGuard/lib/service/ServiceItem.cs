
namespace IPGeoGuard.lib.service;
internal class ServiceItem
{
    public string ServiceName { get; set; }
    public AccessControlList? CountryList { get; set; }
    public AccessControlList? CityList { get; set; }
    public ServiceStat? Stat { get; set; }
    public ServiceItem(string serviceName)
    {
        ServiceName = serviceName;
    }

    internal bool IsAllowed(string country, string city)
    {
        return 
            (
                CountryList == null || 
                (
                    CountryList != null &&
                    CountryList.IsAllowed(country)
                )
            ) &&
            (
                CityList == null || 
                (
                    CityList != null &&
                    CityList.IsAllowed(city)
                )
            );
    }

    internal void IncrementHit(string ip, IPStatInfo info)
    {
        if (!info.Allowed) return;

        if (Stat == null) 
        {
            Stat = new ServiceStat();
        }

        Stat.IncrementHit(ip, info);
    }
}
