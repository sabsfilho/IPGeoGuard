namespace IPGeoGuard.lib.service;
internal class CountryStatItem : AStatItem
{
    public Dictionary<string, CityStatItem>? CityList { get; set; }     
}