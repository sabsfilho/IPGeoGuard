
namespace IPGeoGuard.lib.handler;
internal static class ActionHandler
{
    internal static ActionResponse SubmitRequest(ActionRequest request)
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

    private static ActionResponse DeleteCountryRestriction(ActionRequest request)
    {
        throw new NotImplementedException();
    }

    private static ActionResponse SetCountryRestriction(ActionRequest request, bool v)
    {
        throw new NotImplementedException();
    }

    private static ActionResponse ReadViews(ActionRequest request)
    {
        throw new NotImplementedException();
    }

    private static ActionResponse ReadStatus(ActionRequest request)
    {
        return
            new ActionResponse(){
                Allowed = true,
                Country = "Brazil",
                City = $"Rio {request.IP}"
            };
    }
}