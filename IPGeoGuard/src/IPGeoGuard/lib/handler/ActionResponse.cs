using IPGeoGuard.lib.service;

namespace IPGeoGuard.lib.handler;
public class ActionResponse
{
    public bool? Allowed { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public ServiceStat? Info { get; set; }
    public bool? Updated { get; set; }
}