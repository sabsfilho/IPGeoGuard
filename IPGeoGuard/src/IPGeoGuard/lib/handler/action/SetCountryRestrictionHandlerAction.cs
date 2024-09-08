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
        throw new NotImplementedException();
    }
}