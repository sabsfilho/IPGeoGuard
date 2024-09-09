namespace IPGeoGuard.lib.handler;
public class ActionResponse
{
    public bool Allowed { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
}