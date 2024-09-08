namespace IPGeoGuard.lib.handler;
internal static class ActionHandler
{
    internal static ActionResponse SubmitRequest(ActionRequest request)
    {
        return
            new ActionResponse(){
                Allowed = true,
                Country = "Brazil",
                City = "Rio"
            };
    }
}