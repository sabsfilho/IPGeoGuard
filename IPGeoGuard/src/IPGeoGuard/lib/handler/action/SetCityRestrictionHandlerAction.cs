using IPGeoGuard.lib.service;

namespace IPGeoGuard.lib.handler.action;
internal class SetCityRestrictionHandlerAction : AHandlerAction
{
    private bool block;

    public SetCityRestrictionHandlerAction(ActionRequest request, bool block) 
        : base(request)
    {
        this.block = block;
    }

    public override ActionResponse Request()
    {
        ServiceHandler.Instance.SetCityRestriction(
            ActionRequest.ServiceName,
            ActionRequest.City!,
            block
        );

        return
            new ActionResponse(){
                Updated = true
            };
    }
}