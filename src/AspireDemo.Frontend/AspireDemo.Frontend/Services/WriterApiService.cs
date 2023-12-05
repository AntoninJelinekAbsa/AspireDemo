using AspireDemo.Models.Entities;
using Grpc.Core;

namespace AspireDemo.Frontend.Services;

public class WriterApiService(WriterApi.GrpcWriterApi.WriterApi.WriterApiClient writerApiClient, ILogger<WriterApiService> logger)
{
    public async Task<string> GetPlot(Idea idea)
    {
        var request = new WriterApi.GrpcWriterApi.WriterApiRequest
        {
            Settings = idea.Genre,
            WorkingTitle = idea.WorkingTitle,
            Actors = idea.Actors,
            AdditionalProps = idea.SpecialProps
        };

        try
        {
            var response = await writerApiClient.GetPlotAsync(request);
            logger.LogInformation($"Plot response: {response.Plot}");
            return response.Plot;
        }
        catch (RpcException ex) when (
            // Service name could not be resolved
            ex.StatusCode is StatusCode.Unavailable )
            // Polly resilience timed out after retries
            //|| (ex.StatusCode is StatusCode.Internal && ex.Status.DebugException is TimeoutRejectedException))
        {
            return string.Empty;
        }
    }



}

