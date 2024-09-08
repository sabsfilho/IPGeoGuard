namespace IPGeoGuard.lib.handler.action;
internal class ReadStatusHandlerAction : AHandlerAction
{
    public ReadStatusHandlerAction(ActionRequest request) 
        : base(request)
    {
    }

    public override ActionResponse Request()
    {
        return
            new ActionResponse(){
                Allowed = true,
                Country = "Brazil",
                City = $"Rio v2 {ActionRequest.IP}"
            };
    }
}