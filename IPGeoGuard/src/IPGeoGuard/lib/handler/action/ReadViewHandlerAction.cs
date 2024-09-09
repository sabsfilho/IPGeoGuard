using IPGeoGuard.lib.service;

namespace IPGeoGuard.lib.handler.action;
internal class ReadViewHandlerAction : AHandlerAction
{
    public ReadViewHandlerAction(ActionRequest request) 
        : base(request)
    {
    }

    public override ActionResponse Request()
    {
        var info = ServiceHandler.Instance.GetView(ActionRequest.ServiceName);

        return
            new ActionResponse(){
            };
    }
}