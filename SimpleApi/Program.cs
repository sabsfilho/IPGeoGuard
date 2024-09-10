using Microsoft.AspNetCore.OpenApi;

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

app.MapGet("/GetCurrentTime", () => GetCurrentTime());
app.MapGet("/GetMapViews", () => "GetMapViews");
app.MapPut("/PutRestriction", () => "PutRestriction");
app.MapDelete("/DeleteRestriction", () => "DeleteRestriction");

app.Run();


static string GetCurrentTime()
{
    //return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
    return Request();
}

static string Request()
{
    string uri = "http://127.0.0.1:5050/?message={\"ActionType\":1,\"ServiceName\":\"ServiceName\",\"IP\":\"15.228.198.239\"}";
    var httpClient = new HttpClient();
    
    var tsk = httpClient.GetStringAsync(uri);
    tsk.Wait();
    string json = tsk.Result;
    return json;
}