
using AspireDemo.Models;

namespace AspireDemo.WriterApi;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapPost("/plot", async (HttpContext httpContext, PlotRequest plotRequest) =>
                {
                    var prompt =
                        $"Create script for a TV series with {plotRequest.Settings} settings, starring following actors: {JoinItemsWithAndAtTheEnd(plotRequest.Actors)}. It should also involve {JoinItemsWithAndAtTheEnd(plotRequest.AdditionalProps)}";

                    // set up the client
                    var uri = new Uri(app.Configuration["WriterApiUri"]);
                    var ollama = new OllamaApiClient(uri);

                    // stream a completion and write to the console
                    // keep reusing the context to keep the chat topic going
                    ConversationContext context = null;
                    //context = await ollama.StreamCompletion("How are you today?", "llama2", context, stream => Console.Write(stream.Response));
                    var response = await ollama.GetCompletion(prompt, "grrmartin", context);

                    return Results.Ok(response.Response.Replace("\n", " "));
                })
            .WithName("GenerateNewPlot")
            .WithOpenApi();

            app.Run();
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