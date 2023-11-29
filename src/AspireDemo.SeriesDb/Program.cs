
using AspireDemo.SeriesDb;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<SeriesDbContext>("ideasDb");

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(IdeasDbInitializer.ActivitySourceName));

builder.Services.AddSingleton<IdeasDbInitializer>();

builder.Services.AddHostedService(sp => sp.GetRequiredService<IdeasDbInitializer>());
builder.Services.AddHealthChecks()
    .AddCheck<IdeasDbInitializerHealthCheck>("DbInitializer", null);

var app = builder.Build();

await app.RunAsync();