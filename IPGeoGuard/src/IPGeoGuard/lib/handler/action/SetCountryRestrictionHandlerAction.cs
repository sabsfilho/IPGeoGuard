using IPGeoGuard.lib.service;

namespace IPGeoGuard.lib.handler.action;
internal class SetCountryRestrictionHandlerAction : AHandlerAction
{
    private bool block;

    public SetCountryRestrictionHandlerAction(ActionRequest request, bool block) 
        : base(request)
    {
        this.block = block;
    }

    public override ActionResponse Request()
    {
        ServiceHandler.Instance.SetCountryRestriction(
            ActionRequest.ServiceName,
            ActionRequest.Country!,
            block
        );

        return
            new ActionResponse(){
                Updated = true
            };
    }
}