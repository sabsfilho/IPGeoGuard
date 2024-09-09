using IPGeoGuard.lib.service;

namespace IPGeoGuard.lib.handler.action;
internal class ReadStatusHandlerAction : AHandlerAction
{
    public ReadStatusHandlerAction(ActionRequest request) 
        : base(request)
    {
    }

    public override ActionResponse Request()
    {
        var info = ServiceHandler.Instance.GetInfo(
            ActionRequest.ServiceName,
            ActionRequest.IP, 
            true
        );

        return
            new ActionResponse(){
                Allowed = info.Allowed,
                Country = info.Country,
                City = info.City
            };
    }
}