
using AspireDemo.BossApi;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGrpc();
builder.Services.AddGrpcHealthChecks();


var app = builder.Build();

app.MapGrpcService<BossApiService>();

app.MapGrpcHealthChecksService();

app.MapDefaultEndpoints();

app.Run();

