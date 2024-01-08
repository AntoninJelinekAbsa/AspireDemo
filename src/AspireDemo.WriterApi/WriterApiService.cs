﻿using AspireDemo.WriterApi.GrpcWriterApi;
using Grpc.Core;

namespace AspireDemo.WriterApi;

public class WriterApiService: WriterApi.GrpcWriterApi.WriterApi.WriterApiBase
{
    private readonly ILogger<WriterApiService> _logger;
    private readonly IConfiguration _configuration;

    public WriterApiService(ILogger<WriterApiService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public override async Task<WriterApiResponse> GetPlot(WriterApiRequest request, ServerCallContext context)
    {
        var prompt =
            $"Create a script for an epic TV series, genre: {request.Settings} and title: {request.WorkingTitle}. Starring actors: {request.Actors}. It should also involve {request.AdditionalProps}";

        _logger.LogInformation($"Prompt: {prompt}");

        // set up the client
        var uri = new Uri(_configuration["WriterApiUri"]);
        var ollama = new OllamaApiClient(uri);

        // stream a completion and write to the console
        // keep reusing the context to keep the chat topic going
        ConversationContext conversationContextontext = null;
        //context = await ollama.StreamCompletion("How are you today?", "llama2", context, stream => Console.Write(stream.Response));
        var response = await ollama.GetCompletion(prompt, "GRRMARTIN", conversationContextontext);

        _logger.LogInformation($"Response: {response.Response}");

        return new WriterApiResponse
        {
            Plot = response.Response.Replace("\n", " ")
        };
    }


    public override async Task GetPlotStream(WriterApiRequest request, IServerStreamWriter<WriterApiResponse> responseStream, ServerCallContext context)
    {
        var prompt =
            $"Create script for a TV series, genre: {request.Settings} and title: {request.WorkingTitle}. Starring actors: {request.Actors}. It should also involve {request.AdditionalProps}";

        _logger.LogInformation($"Prompt: {prompt}");

        // set up the client
        var uri = new Uri(_configuration["WriterApiUri"]);
        var ollama = new OllamaApiClient(uri);

        // stream a completion and write to the console
        // keep reusing the context to keep the chat topic going
        ConversationContext conversationContextontext = null;
        conversationContextontext = await ollama.StreamCompletion(prompt, "GRRMARTIN", conversationContextontext,
            stream =>
            {
                responseStream.WriteAsync(new WriterApiResponse
                {
                    Plot = stream.Response.Replace("\n", " ")
                });
            });

        _logger.LogInformation($"Ollama completed response.");
        context.Status = new Status(StatusCode.OK, "Ollama completed response.");
    }
}
