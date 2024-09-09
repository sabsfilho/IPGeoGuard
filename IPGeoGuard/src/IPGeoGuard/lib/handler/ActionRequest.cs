namespace IPGeoGuard.lib.handler;
public class ActionRequest
{
    public required ActionTypeEnum ActionType { get; set; }
    public required string ServiceName { get; set; }
    public string? IP { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
}