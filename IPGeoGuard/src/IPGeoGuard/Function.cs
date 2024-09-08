using Amazon.Lambda.Core;
using IPGeoGuard.lib.handler;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace IPGeoGuard;

public class Function
{
    
    /// <summary>
    /// A simple function that takes an ActionRequest JSON string and returns an ActionResponse JSON string.
    /// ActionType
    /// ReadStatus = 1, check if the requested IP is allowed to retrieve Service data.
    /// ReadViews = 2, read IP hits by Country and City
    /// BlockCountry = 3, block request from Country to defined Service
    /// PermitCountry = 4, permit request from Country to defined Service
    /// DeleteCountryRole = 5, revoke Country role
    /// </summary>
    /// <param name="request">ActionRequest, {"ActionType":1,"ServiceName":"ServiceName","IP":"0.0.0.0"}</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    //public ActionResponse FunctionHandler(string input, ILambdaContext context)
    public ActionResponse FunctionHandler(ActionRequest request, ILambdaContext context)
    {
        var response = ActionHandler.SubmitRequest(request);

        return response;
    }
}
