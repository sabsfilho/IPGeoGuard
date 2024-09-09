using IPGeoGuard.lib.service;

namespace IPGeoGuard.lib.handler.action;
internal class DeleteCityRestrictionHandlerAction : AHandlerAction
{
    public DeleteCityRestrictionHandlerAction(ActionRequest request) 
        : base(request)
    {
    }

    public override ActionResponse Request()
    {
        ServiceHandler.Instance.DeleteCityRestriction(
            ActionRequest.ServiceName,
            ActionRequest.City!
        );

        return
            new ActionResponse(){
                Updated = true
            };
    }
}