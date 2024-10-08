using IPGeoGuard.lib.service;

namespace IPGeoGuard.lib.handler.action;
internal class DeleteCountryRestrictionHandlerAction : AHandlerAction
{
    public DeleteCountryRestrictionHandlerAction(ActionRequest request) 
        : base(request)
    {
    }

    public override ActionResponse Request()
    {
        ServiceHandler.Instance.DeleteCountryRestriction(
            ActionRequest.ServiceName,
            ActionRequest.Country!
        );

        return
            new ActionResponse(){
                Updated = true
            };
    }
}