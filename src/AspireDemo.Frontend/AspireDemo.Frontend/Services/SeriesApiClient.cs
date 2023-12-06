using System.Text.Json;
using AspireDemo.Models.Entities;

namespace AspireDemo.Frontend.Services;

public class SeriesApiClient(HttpClient client, ILogger<SeriesApiClient> logger)
{
    private const string SeriesApiBase = "/api/v1/series";
    private const string StaticDataApiBase = "/api/v1/static-data";


    public async Task<ICollection<Idea>> GetIdeasAsync()
    {
        var ideas = await client.GetFromJsonAsync<Idea[]>(SeriesApiBase);

        if (ideas != null)
        {
            logger.LogInformation("SeriesApiClient.GetIdeasAsync: {0}", ideas.Length);
            return ideas;
        }

        logger.LogInformation("SeriesApiClient.GetIdeasAsync: series is null");
        return new List<Idea>();
    }

    public async Task<bool> AddIdeasAsync(Idea idea)
    {
        var addIdeaResponse = await client.PostAsJsonAsync<Idea>(SeriesApiBase, idea);

        if (addIdeaResponse.IsSuccessStatusCode)
        {
            logger.LogInformation("New idea successfully submitted");
            return true;
        }

        logger.LogError("Failed to submit new idea. " + addIdeaResponse.StatusCode );
        return false;
    }

    public async Task<bool> UpdateIdea(Idea idea)
    {
        logger.LogInformation("SeriesApiClient.UpdateIdea: {0}", JsonSerializer.Serialize(idea));
        var updateIdeaResponse = await client.PutAsJsonAsync(SeriesApiBase, idea);

        if (updateIdeaResponse.IsSuccessStatusCode)
        {
            logger.LogInformation("New idea successfully updated with plot");
            return true;
        }

        logger.LogError("Failed to update idea. " + updateIdeaResponse.StatusCode);
        return false;
    }

    public async Task<List<Genre>> GetGenresAsync()
    {
        var genres = await client.GetFromJsonAsync<Genre[]>(StaticDataApiBase + "/genres");

        if (genres != null)
        {
            logger.LogInformation("SeriesApiClient.GetGenresAsync: {0}", genres.Length);
            return genres.ToList();
        }

        logger.LogInformation("SeriesApiClient.GetGenresAsync: genres is null");
        return new List<Genre>();
    }

    public async Task<List<Actor>> GetActorsAsync()
    {
        var actors = await client.GetFromJsonAsync<Actor[]>(StaticDataApiBase + "/actors");

        if (actors != null)
        {
            logger.LogInformation("SeriesApiClient.GetActorsAsync: {0}", actors.Length);
            return actors.ToList();
        }

        logger.LogInformation("SeriesApiClient.GetActorsAsync: actors is null");
        return new List<Actor>();
    }

    public async Task<List<SpecialProp>> GetSpecialPropsAsync()
    {
        var specialProps = await client.GetFromJsonAsync<SpecialProp[]>(StaticDataApiBase + "/special-props");

        if (specialProps != null)
        {
            logger.LogInformation("SeriesApiClient.GetSpecialPropsAsync: {0}", specialProps.Length);
            return specialProps.ToList();
        }

        logger.LogInformation("SeriesApiClient.GetSpecialPropsAsync: genres is null");
        return new List<SpecialProp>();
    }
}
