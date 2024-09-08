namespace IPGeoGuard.lib.handler.action;
internal abstract class AHandlerAction
{
    public abstract ActionResponse Request();

    public ActionRequest ActionRequest { get; private set; }

    public AHandlerAction(ActionRequest request)
    {
        this.ActionRequest = request;
    }
}