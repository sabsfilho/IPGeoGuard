namespace SimpleApi;
public class ActionResponse
{
    public bool? Allowed { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public bool? Updated { get; set; }
    public string? errorMessage { get; set; }
}