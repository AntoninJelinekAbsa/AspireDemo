using AspireDemo.BossApi.GrpcBossApi;
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
        var prompt = $"Analyze following TV series plot and decide whether you like it or not: {request.Plot}";

        _logger.LogInformation($"Prompt: {prompt}");

        // set up the client
        var uri = new Uri(_configuration["BossApiUri"]);
        var ollama = new OllamaApiClient(uri);

        // stream a completion and write to the console
        // keep reusing the context to keep the chat topic going
        ConversationContext conversationContextontext = null;
        //context = await ollama.StreamCompletion("How are you today?", "llama2", context, stream => Console.Write(stream.Response));
        var response = await ollama.GetCompletion(prompt, "nikola", conversationContextontext);

        _logger.LogInformation($"Response: {response.Response}");

        return new ReviewResponse
        {
            Result = response.Response.Replace("\n", " ")
        };
    }
}

