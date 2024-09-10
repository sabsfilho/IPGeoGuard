
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using SimpleApi;

const string GEOGUARD_API_URL = "https://ab9kglpck4.execute-api.sa-east-1.amazonaws.com/v1/read-status";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/GetCurrentTime", (HttpContext httpContext) => GetCurrentTime(httpContext, null));
app.MapGet("/GetCurrentTime/{ip}", (HttpContext httpContext, string ip) => GetCurrentTime(httpContext, ip));
app.MapGet("/GetMapViews/{serviceName}", (string serviceName) => GetMapViews(serviceName));
app.MapPut("/PutRestriction/{serviceName}/{countryList}", (string serviceName, string countryList) => SetRestriction(serviceName, countryList, false));
app.MapDelete("/DeleteRestriction/{serviceName}/{countryList}", (string serviceName, string countryList) => SetRestriction(serviceName, countryList, true));

app.Run();

static string SetRestriction(string serviceName, string countryList, bool remove)
{
    return
        Request(new ActionRequest(){
            ServiceName = serviceName,
            ActionType = remove ? 5 : 3,
            Country = countryList
        });
}

static string GetMapViews(string serviceName)
{
    return
        Request(new ActionRequest(){
            ServiceName = serviceName,
            ActionType = 2
        });
}


static ContentHttpResult GetCurrentTime(HttpContext httpContext, string? ip)
{
    if (string.IsNullOrEmpty(ip))
    {
        var ripa = httpContext.Connection.RemoteIpAddress;        
        if (ripa != null)
        {
            ip = ripa.ToString();
        }
    }
    if (string.IsNullOrEmpty(ip))
    {
        return BuildForbiddenMessage("undefined ip");
    }
    if (ip == "127.0.0.1")
    {
        return BuildForbiddenMessage("localhost");
    }
    var r = Request(new ActionRequest(){
        ServiceName = "SimpleApi-GetCurrentTime",
        ActionType = 1,
        IP = ip
    });
    var o = JsonSerializer.Deserialize<ActionResponse>(r)!;
    if (o.errorMessage != null)
    {
        return BuildErrorMessage(o.errorMessage);
    }
    var allowed = o.Allowed;
    if (allowed == null)
    {
        return BuildErrorMessage("allowed undefined");
    }
    
    if (!allowed.Value)
    {
        return BuildForbiddenMessage("geolocation forbidden");
    }
    string msg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    return BuildOkMessage(msg);
}
static ContentHttpResult BuildOkMessage(string msg)
{
    return
        BuildHttpMessage(msg, System.Net.HttpStatusCode.OK);
}

static ContentHttpResult BuildForbiddenMessage(string msg)
{
    return
        BuildHttpMessage(msg, System.Net.HttpStatusCode.Forbidden);
}

static ContentHttpResult BuildErrorMessage(string msg)
{
    return
        BuildHttpMessage(msg, System.Net.HttpStatusCode.InternalServerError);
}

static ContentHttpResult BuildHttpMessage(string msg, System.Net.HttpStatusCode statusCode)
{
    return
        TypedResults.Content(
            content: msg,
            contentType: "text/html",
            statusCode: (int?)statusCode
        );
}

static string Request(ActionRequest actionRequest)
{
    var r = RequestAsync(actionRequest);
    r.Wait();
    return r.Result;
}
static async Task<string> RequestAsync(ActionRequest actionRequest)
{
    string request = JsonSerializer.Serialize(actionRequest);

    using(var httpClient = new HttpClient())
    {
        var content = new StringContent(
            request,
            Encoding.UTF8,
            "application/json"
        );
        var tsk = await httpClient.PostAsync(GEOGUARD_API_URL, content);        
        string json = await tsk.Content.ReadAsStringAsync();
        return json;
    }
}