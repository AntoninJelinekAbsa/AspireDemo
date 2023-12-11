using AspireDemo.Models.Entities;
using Grpc.Core;


namespace AspireDemo.Frontend.Services;

public class BossApiService(BossApi.GrpcBossApi.BossApi.BossApiClient bossApiClient, ILogger<BossApiService> logger)
{
    public async Task<string> GetReview(Idea idea)
    {
        var request = new BossApi.GrpcBossApi.ReviewRequest
        {
            Plot = idea.Plot
        };

        try
        {
            var response = await bossApiClient.GetReviewAsync(request);
            logger.LogInformation($"Review response: {response.Result}");
            return response.Result;
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

    public Action<string> ReviewUpdateReceivedCallback { get; set; }

    public async Task GetReviewStream(Idea idea, CancellationToken ct)
    {
        var request = new BossApi.GrpcBossApi.ReviewRequest
        {
            Plot = idea.Plot
        };

        using var streamingCall = bossApiClient.GetReviewStream(request, cancellationToken: ct);

        try
        {
            await foreach (var reviewData in streamingCall.ResponseStream.ReadAllAsync(cancellationToken: ct))
            {
                if (ReviewUpdateReceivedCallback != null)
                {
                    ReviewUpdateReceivedCallback(reviewData.Result);
                }
            }
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            throw;
        }
    }
}

