
using AspireDemo.SeriesDb;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<SeriesDbContext>("seriesDb");

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(IdeasDbInitializer.ActivitySourceName));

builder.Services.AddSingleton<IdeasDbInitializer>();

builder.Services.AddHostedService(sp => sp.GetRequiredService<IdeasDbInitializer>());
builder.Services.AddHealthChecks()
    .AddCheck<IdeasDbInitializerHealthCheck>("DbInitializer", null);

var app = builder.Build();

app.MapDefaultEndpoints();

await app.RunAsync();