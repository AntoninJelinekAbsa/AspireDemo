var builder = DistributedApplication.CreateBuilder(args);

const string writeAiApiEndpointName = "writerAiApi";
const string bossAiApiEndpointName = "bossAiApi";

var seriesDb = builder.AddPostgresContainer("postgres").AddDatabase("seriesdb");
var seriesService = builder.AddProject<Projects.AspireDemo_SeriesService>("seriesService").WithReference(seriesDb);

var writerAi = builder.AddContainer("writerAiApi", "grrmartin-real")
        .WithServiceBinding(containerPort: 11434, hostPort: 11434, name: writeAiApiEndpointName, scheme: "http");

var bossAi = builder.AddContainer("bossAiApi", "simpleboss-real")
    .WithServiceBinding(containerPort: 11434, hostPort: 11435, name: bossAiApiEndpointName, scheme: "http");

var writerApi = builder.AddProject<Projects.AspireDemo_WriterApi>("writerApi")
    .WithEnvironment("WriterApiUri", writerAi.GetEndpoint(writeAiApiEndpointName));

var boosApi = builder.AddProject<Projects.AspireDemo_BossApi>("bossApi")
    .WithEnvironment("BossApiUri", bossAi.GetEndpoint(bossAiApiEndpointName));


var frontend = builder.AddProject<Projects.AspireDemo_Frontend>("frontend")
    .WithReference(seriesService)
    .WithReference(writerApi)
    .WithReference(boosApi);

builder.AddProject<Projects.AspireDemo_SeriesDb>("seriesdbapp")
    .WithReference(seriesDb);


builder.Build().Run();
