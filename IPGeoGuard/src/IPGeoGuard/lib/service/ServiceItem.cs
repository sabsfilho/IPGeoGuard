namespace IPGeoGuard.lib.service;
internal class ServiceItem
{
    public List<string>? BlockedList { get; set; }
    public List<string>? PermittedList { get; set; }
    public ServiceStat? Stat { get; set; }
}

internal class ServiceStat
{
    public List<string>? IPList { get; set; }
    public List<CountryStatItem>? CountryList { get; set; }
}

internal abstract class AStatItem
{
    public required string Name { get; set; }
    public int Hits { get; set; }
    
}

internal class CountryStatItem : AStatItem
{
    public List<CityStatItem>? CityList { get; set; }     
}
internal class CityStatItem : AStatItem
{
}