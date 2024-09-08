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

app.MapGet("/GetCurrentTime", () => "GetCurrentTime");
app.MapGet("/GetMapViews", () => "GetMapViews");
app.MapPut("/PutRestriction", () => "PutRestriction");
app.MapDelete("/DeleteRestriction", () => "DeleteRestriction");

app.Run();
