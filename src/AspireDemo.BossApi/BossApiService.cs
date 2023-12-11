﻿using AspireDemo.BossApi.GrpcBossApi;
using Grpc.Core;

namespace AspireDemo.BossApi;

public class BossApiService : GrpcBossApi.BossApi.BossApiBase
{
    private readonly ILogger<BossApiService> _logger;
    private readonly IConfiguration _configuration;

    public BossApiService(ILogger<BossApiService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public override async Task<ReviewResponse> GetReview(ReviewRequest request, ServerCallContext context)
    {
        var prompt = $"{request.Plot}";

        _logger.LogInformation($"Prompt: {prompt}");

        // set up the client
        var uri = new Uri(_configuration["BossApiUri"]);
        var ollama = new OllamaApiClient(uri);

        // stream a completion and write to the console
        // keep reusing the context to keep the chat topic going
        ConversationContext conversationContextontext = null;
        var response = await ollama.GetCompletion(prompt, "simpleboss", conversationContextontext);

        _logger.LogInformation($"Response: {response.Response}");

        return new ReviewResponse
        {
            Result = response.Response.Replace("\n", " ")
        };
    }


    public override async Task GetReviewStream(ReviewRequest request, IServerStreamWriter<ReviewResponse> responseStream, ServerCallContext context)
    {
        var prompt = $"{request.Plot}";

        _logger.LogInformation($"Prompt: {prompt}");

        // set up the client
        var uri = new Uri(_configuration["BossApiUri"]);
        var ollama = new OllamaApiClient(uri);

        // stream a completion and write to the console
        // keep reusing the context to keep the chat topic going
        ConversationContext conversationContextontext = null;
        conversationContextontext = await ollama.StreamCompletion(prompt, "simpleboss", conversationContextontext,
            stream => {
                responseStream.WriteAsync(new ReviewResponse
                {
                    Result = stream.Response.Replace("\n", " ")
                });
            });

        _logger.LogInformation($"Ollama completed response.");
        context.Status = new Status(StatusCode.OK, "Ollama completed response.");
    }
}

