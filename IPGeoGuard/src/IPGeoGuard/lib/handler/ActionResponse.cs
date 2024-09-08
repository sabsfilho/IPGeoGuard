namespace IPGeoGuard.lib.handler;
public class ActionResponse
{
    public required bool Allowed { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
}