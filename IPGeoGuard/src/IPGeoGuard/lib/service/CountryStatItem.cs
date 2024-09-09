namespace IPGeoGuard.lib.service;
public class CountryStatItem : AStatItem
{
    public Dictionary<string, CityStatItem>? CityList { get; set; }     
}