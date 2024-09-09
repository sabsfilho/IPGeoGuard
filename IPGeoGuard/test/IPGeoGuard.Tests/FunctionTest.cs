using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using IPGeoGuard.lib.handler;

namespace IPGeoGuard.Tests;

public class FunctionTest
{
    [Fact]
    public void TestReadStatusFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();

        var request = new ActionRequest()
        {
            ActionType = ActionTypeEnum.ReadStatus,
            ServiceName = "TestService",
            IP = "15.228.198.239"
        };

        var response = function.FunctionHandler(request, context);

        Assert.Equal("Sao Paulo", response.City);
    }
}