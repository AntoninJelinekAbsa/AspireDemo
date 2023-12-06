
using AspireDemo.BossApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcHealthChecks();


var app = builder.Build();

app.MapGrpcService<BossApiService>();

app.MapGrpcHealthChecksService();

app.Run();

