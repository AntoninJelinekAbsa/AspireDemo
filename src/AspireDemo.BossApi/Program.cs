
using AspireDemo.Models;

namespace AspireDemo.BossApi
{
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

            app.MapPost("/plot", async (HttpContext httpContext, ReviewRequest reviewRequest) =>
                {
                    var prompt =
                        $"Analyze following TV serie plot and decide whether you like it or not: {reviewRequest.Plot}";

                    // set up the client
                    var uri = new Uri(app.Configuration["BossApiUri"]);
                    var ollama = new OllamaApiClient(uri);

                    // stream a completion and write to the console
                    // keep reusing the context to keep the chat topic going
                    ConversationContext context = null;
                    var response = await ollama.GetCompletion(prompt, "elon", context);

                    return Results.Ok(response.Response.Replace("\n", " "));
                })
                .WithName("ReviewPlot")
                .WithOpenApi();

            app.Run();
        }
    }
}
