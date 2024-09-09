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
            case ActionTypeEnum.ReadView:
                return ReadView(request);
            case ActionTypeEnum.BlockCountry:
                return SetCountryRestriction(request, true);
            case ActionTypeEnum.PermitCountry:
                return SetCountryRestriction(request, false);
            case ActionTypeEnum.DeleteCountryRestriction:
                return DeleteCountryRestriction(request);
            case ActionTypeEnum.BlockCity:
                return SetCityRestriction(request, true);
            case ActionTypeEnum.PermitCity:
                return SetCityRestriction(request, false);
            case ActionTypeEnum.DeleteCityRestriction:
                return DeleteCityRestriction(request);
        }
        throw new NotImplementedException();
    }
    private static AHandlerAction DeleteCityRestriction(ActionRequest request)
    {
        return new DeleteCityRestrictionHandlerAction(request);
    }

    private static AHandlerAction SetCityRestriction(ActionRequest request, bool block)
    {
        return new SetCityRestrictionHandlerAction(request, block);
    }

    private static AHandlerAction DeleteCountryRestriction(ActionRequest request)
    {
        return new DeleteCountryRestrictionHandlerAction(request);
    }

    private static AHandlerAction SetCountryRestriction(ActionRequest request, bool block)
    {
        return new SetCountryRestrictionHandlerAction(request, block);
    }

    private static AHandlerAction ReadView(ActionRequest request)
    {
        return new ReadViewHandlerAction(request);
    }

    private static AHandlerAction ReadStatus(ActionRequest request)
    {
        return new ReadStatusHandlerAction(request);
    }
}


