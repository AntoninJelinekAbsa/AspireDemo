

using AspireDemo.WriterApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcHealthChecks();


var app = builder.Build();

app.MapGrpcService<WriterApiService>();

app.MapGrpcHealthChecksService();


app.Run();

