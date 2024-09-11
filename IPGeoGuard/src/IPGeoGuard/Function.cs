using Amazon.Lambda.Core;
using IPGeoGuard.lib.handler;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace IPGeoGuard;

public class Function
{
    // cache respository name
    internal const string IPGEOGUARD_LOCAL_STORAGE_REPO_NAME = "IPGeoGuardRepo";
    internal const string IPGEOGUARD_S3_STORAGE_REPO_NAME = "ipgeoguardrepo";

    // "IPGeoGuardRepo" directory will be locally created to store Service information
    // Do not enable it before publishing to AWS Lambda because its read-only file system
    internal const bool USE_LOCAL_FILE_STORAGE_CACHE = false;

    // "IPGeoGuardRepo" directory will be created on AWS S3 to storage Service information
    // aws s3api create-bucket --bucket ipgeoguardrepo --region sa-east-1 --create-bucket-configuration LocationConstraint=sa-east-1
    // grant AmazonS3FullAccess policy to LambdaUser
    internal const bool USE_S3_STORAGE_CACHE = true;

    // use Redis in-memory database for caching performance optimization
    // to use this feature you must configure AWS MEMORYDB and install .NET Client for Redis
    internal const bool USE_REDIS_IN_MEMORY_STORAGE_CACHE = false;
    
    /// <summary>
    /// A simple function that takes an ActionRequest JSON string and returns an ActionResponse JSON string.
    /// ActionType
    /// ReadStatus = 1, check if the requested IP is allowed to retrieve Service data.
    /// ReadViews = 2, read IP hits by Country and City
    /// BlockCountry = 3, block request from Country to defined Service
    /// PermitCountry = 4, permit request from Country to defined Service
    /// DeleteCountryRestriction = 5, revoke Country role
    /// BlockCity = 6, block request from City to defined Service
    /// PermitCity = 7, permit request from City to defined Service
    /// DeleteCityRestriction = 8, revoke City role
    /// </summary>
    /// <param name="request">ActionRequest, {"ActionType":1,"ServiceName":"ServiceName","IP":"15.228.198.239"}</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    //public ActionResponse FunctionHandler(string input, ILambdaContext context)
    public ActionResponse FunctionHandler(ActionRequest request, ILambdaContext context)
    {
        var response = ActionHandler.SubmitRequest(request);

        return response;
    }
}
