namespace IPGeoGuard.lib.handler;
public class ActionRequest
{
    public required ActionTypeEnum ActionType { get; set; }
    public required string ServiceName { get; set; }
    public required string IP { get; set; }
}