using AspireDemo.Frontend.Components;
using AspireDemo.Frontend.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using AspireDemo.Frontend.Extensions;
using AspireDemo.Frontend.Grid;

using AspireDemo.WriterApi.GrpcWriterApi;
using AspireDemo.BossApi.GrpcBossApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<WriterApiService>()
    .AddGrpcServiceReference<WriterApi.WriterApiClient>("https://localhost:7292", failureStatus: HealthStatus.Degraded);

builder.Services.AddSingleton<BossApiService>()
    .AddGrpcServiceReference<BossApi.BossApiClient>("https://localhost:7293", failureStatus: HealthStatus.Degraded);

builder.Services.AddHttpServiceReference<SeriesApiClient>("https://localhost:7122", healthRelativePath: "readiness");

// Pager
builder.Services.AddScoped<IPageHelper, PageHelper>();

// Filters
builder.Services.AddScoped<IIdeaFilters, GridControls>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(AspireDemo.Frontend.Client._Imports).Assembly);

app.Run();






