namespace IPGeoGuard.lib.service;
internal class ServiceItem
{
    public List<string>? BlockedList { get; set; }
    public List<string>? PermittedList { get; set; }
    public ServiceStat? Stat { get; set; }
}
