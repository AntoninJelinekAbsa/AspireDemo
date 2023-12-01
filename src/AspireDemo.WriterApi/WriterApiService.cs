using AspireDemo.WriterApi.GrpcWriterApi;
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
            $"Create script for a TV series with {request.Settings} settings, starring following actors: {JoinItemsWithAndAtTheEnd(request.Actors.ToList())}. It should also involve {JoinItemsWithAndAtTheEnd(request.AdditionalProps.ToList())}";

        // set up the client
        var uri = new Uri(_configuration["WriterApiUri"]);
        var ollama = new OllamaApiClient(uri);

        // stream a completion and write to the console
        // keep reusing the context to keep the chat topic going
        ConversationContext conversationContextontext = null;
        //context = await ollama.StreamCompletion("How are you today?", "llama2", context, stream => Console.Write(stream.Response));
        var response = await ollama.GetCompletion(prompt, "grrmartin", conversationContextontext);

        return new WriterApiResponse
        {
            Plot = response.Response.Replace("\n", " ")
        };
    }


    private static string JoinItemsWithAndAtTheEnd(List<string> items)
    {
        var actorsString = string.Join(", ", items);

        if (items.Count > 1)
        {
            actorsString = actorsString.ReplaceLastOccurrence(", ", " and ");
        }

        return actorsString;
    }
}

public static class StringExtensions
{
    public static string ReplaceLastOccurrence(this string source, string find, string replace)
    {
        int place = source.LastIndexOf(find, StringComparison.InvariantCultureIgnoreCase);

        if (place == -1)
        {
            return source;
        }

        return source.Remove(place, find.Length).Insert(place, replace);
    }
}