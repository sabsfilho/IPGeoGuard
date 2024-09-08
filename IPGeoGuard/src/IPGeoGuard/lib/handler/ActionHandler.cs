using IPGeoGuard.lib.handler.action;

namespace IPGeoGuard.lib.handler;
internal static class ActionHandler
{
    internal static ActionResponse SubmitRequest(ActionRequest request)
    {
        var action = GetHandlerAction(request);
        var response = action.Request();
        return response;
    }

    private static AHandlerAction GetHandlerAction(ActionRequest request)
    {
        switch(request.ActionType)
        {
            case ActionTypeEnum.ReadStatus:
                return ReadStatus(request);
            case ActionTypeEnum.ReadViews:
                return ReadViews(request);
            case ActionTypeEnum.BlockCountry:
                return SetCountryRestriction(request, true);
            case ActionTypeEnum.PermitCountry:
                return SetCountryRestriction(request, false);
            case ActionTypeEnum.DeleteCountryRestriction:
                return DeleteCountryRestriction(request);
        }
        throw new NotImplementedException();
    }

    private static AHandlerAction DeleteCountryRestriction(ActionRequest request)
    {
        return new DeleteCountryRestrictionHandlerAction(request);
    }

    private static AHandlerAction SetCountryRestriction(ActionRequest request, bool block)
    {
        return new SetCountryRestrictionHandlerAction(request, block);
    }

    private static AHandlerAction ReadViews(ActionRequest request)
    {
        return new ReadViewsHandlerAction(request);
    }

    private static AHandlerAction ReadStatus(ActionRequest request)
    {
        return new ReadStatusHandlerAction(request);
    }
}


