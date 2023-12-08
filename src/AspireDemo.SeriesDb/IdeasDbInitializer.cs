using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AspireDemo.SeriesDb;

internal class IdeasDbInitializer(IServiceProvider serviceProvider, ILogger<IdeasDbInitializer> logger):BackgroundService
{
    public const string ActivitySourceName = "Migrations";

    private readonly ActivitySource _activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SeriesDbContext>();

        await InitializeDatabaseAsync(dbContext, cancellationToken);
    }

    private async Task InitializeDatabaseAsync(SeriesDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        using var activity = _activitySource.StartActivity("Migrating catalog database", ActivityKind.Client);

        var sw = Stopwatch.StartNew();

        await strategy.ExecuteAsync(() => dbContext.Database.MigrateAsync(cancellationToken));

        await SeedAsync(dbContext, cancellationToken);

        logger.LogInformation("Database initialization completed after {ElapsedMilliseconds}ms", sw.ElapsedMilliseconds);
    }

    private async Task SeedAsync(SeriesDbContext dbContext, CancellationToken cancellationToken)
    {
        logger.LogInformation("Seeding database");

        static List<Genre> GetPreconfiguredGenres()
        {
            return new List<Genre>
            {
                new Genre() {Id =1, Name = "Action" },
                new Genre() {Id =2, Name = "Comedy" },
                new Genre() {Id =3, Name = "Drama" },
                new Genre() {Id =4, Name = "Fantasy" },
                new Genre() {Id =5, Name = "Horror" },
                new Genre() {Id =6, Name = "Mystery" },
                new Genre() {Id =7, Name = "Romance" },
                new Genre() {Id =8, Name = "Thriller" },
                new Genre() {Id =9, Name = "Western" },
                new Genre() {Id =10, Name = "Science-Fiction" }
            };
        }

        static List<SpecialProp> GetPreconfiguredSpecialProps()
        {
            return new List<SpecialProp>
            {
                new SpecialProp() {Id = 1, Name = "Cars" },
                new SpecialProp() {Id = 2, Name = "Horses" },
                new SpecialProp() {Id = 3, Name = "Spaceships" },
                new SpecialProp() {Id = 4, Name = "Swords" },
                new SpecialProp() {Id = 5, Name = "Wands" },
                new SpecialProp() {Id = 6, Name = "Dragons" },
                new SpecialProp() {Id = 7, Name = "Lightsabres" },
                new SpecialProp() {Id = 8, Name = "Computers" },
                new SpecialProp() {Id = 9, Name = "Redis databases" }
            };
        }

        static List<Actor> GetPreconfiguredActors()
        {
            return new List<Actor>
            {
                new Actor() { FirstName = "Tom", LastName = "Hanks" },
                new Actor() { FirstName = "Brad", LastName = "Pitt" },
                new Actor() { FirstName = "Morgan", LastName = "Freeman" },
                new Actor() { FirstName = "Tom", LastName = "Cruise" },
                new Actor() { FirstName = "Leonardo", LastName = "DiCaprio" },
                new Actor() { FirstName = "Bruce", LastName = "Willis" },
                new Actor() { FirstName = "Johnny", LastName = "Depp" },
                new Actor() { FirstName = "Al", LastName = "Pacino" },
                new Actor() { FirstName = "Robert", LastName = "De Niro" },
                new Actor() { FirstName = "Kevin", LastName = "Spacey" },
                new Actor() { FirstName = "Denzel", LastName = "Washington" },
                new Actor() { FirstName = "Russell", LastName = "Crowe" },
                new Actor() { FirstName = "Christian", LastName = "Bale" },
                new Actor() { FirstName = "Edward", LastName = "Norton" },
                new Actor() { FirstName = "Liam", LastName = "Neeson" },
                new Actor() { FirstName = "Matt", LastName = "Damon" },
                new Actor() { FirstName = "Harrison", LastName = "Ford" },
                new Actor() { FirstName = "Clint", LastName = "Eastwood" },
                new Actor() { FirstName = "Sean", LastName = "Connery" },
                new Actor() { FirstName = "Anthony", LastName = "Hopkins" },
                new Actor() { FirstName = "Jack", LastName = "Nicholson" },
                new Actor() { FirstName = "Will", LastName = "Smith" },
                new Actor() { FirstName = "Robin", LastName = "Williams" },
                new Actor() { FirstName = "Jim", LastName = "Carrey" },
                new Actor() { FirstName = "Samuel", LastName = "Jackson" },
                new Actor() { FirstName = "Keanu", LastName = "Reeves" },
                new Actor() { FirstName = "John", LastName = "Travolta" },
                new Actor() { FirstName = "Nicolas", LastName = "Cage" },
                new Actor() { FirstName = "Sylvester", LastName = "Stallone" },
                new Actor() { FirstName = "Arnold", LastName = "Schwarzenegger" },
                new Actor() { FirstName = "Bruce", LastName = "Lee" },
                new Actor() { FirstName = "Jet", LastName = "Li" },
                new Actor() { FirstName = "Jackie", LastName = "Chan" },
                new Actor() { FirstName = "Jason", LastName = "Statham" },
                new Actor() { FirstName = "Wesley", LastName = "Snipes" },
                new Actor() { FirstName = "Jean-Claude", LastName = "Van Damme" },
                new Actor() { FirstName = "Steven", LastName = "Seagal" },
                new Actor() { FirstName = "Dwayne", LastName = "Johnson" },
                new Actor() { FirstName = "Vin", LastName = "Diesel" },
                new Actor { FirstName = "Meryl", LastName = "Streep" },
                new Actor { FirstName = "Cate", LastName = "Blanchett" },
                new Actor { FirstName = "Nicole", LastName = "Kidman" },
                new Actor { FirstName = "Viola", LastName = "Davis" },
                new Actor { FirstName = "Natalie", LastName = "Portman" },
                new Actor { FirstName = "Kate", LastName = "Winslet" },
                new Actor { FirstName = "Charlize", LastName = "Theron" },
                new Actor { FirstName = "Emma", LastName = "Stone" },
                new Actor { FirstName = "Anne", LastName = "Hathaway" },
                new Actor { FirstName = "Julianne", LastName = "Moore" },
                new Actor { FirstName = "Sandra", LastName = "Bullock" },
                new Actor { FirstName = "Helen", LastName = "Mirren" },
                new Actor { FirstName = "Tilda", LastName = "Swinton" },
                new Actor { FirstName = "Jodie", LastName = "Foster" },
                new Actor { FirstName = "Judi", LastName = "Dench" },
                new Actor { FirstName = "Frances", LastName = "McDormand" },
                new Actor { FirstName = "Marion", LastName = "Cotillard" },
                new Actor { FirstName = "Rachel", LastName = "Weisz" },
                new Actor { FirstName = "Penelope", LastName = "Cruz" },
                new Actor { FirstName = "Halle", LastName = "Berry" },
                new Actor { FirstName = "Jennifer", LastName = "Lawrence" },
            };
        }

        static Idea GetPreconfiguredIdea()
        {
            return new Idea
            {
                WorkingTitle = "Romance on Nile",
                Genre = "Western",
                Actors = "Arnold Schwarzenegger, Sandra Bullock and Sean Connery",
                SpecialProps = "Spaceships, Horses and Redis databases"
            };
        }

        if (!dbContext.Genres.Any())
        {
            var genres = GetPreconfiguredGenres();
            await dbContext.Genres.AddRangeAsync(genres, cancellationToken);

            logger.LogInformation("Seeding {CatalogGenresCount} genres", genres.Count);

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        if (!dbContext.SpecialProps.Any())
        {
            var props = GetPreconfiguredSpecialProps();
            await dbContext.SpecialProps.AddRangeAsync(props, cancellationToken);

            logger.LogInformation("Seeding {SpecialPropsCount} special props", props.Count);

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        if (!dbContext.Actors.Any())
        {
            var actors = GetPreconfiguredActors();

            for (int i = 0; i < actors.Count; i++)
            {
                actors[i].Id = i + 1;
            }

            await dbContext.Actors.AddRangeAsync(actors, cancellationToken);

            logger.LogInformation("Seeding {ActorsCount} actors", actors.Count);

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        if (!dbContext.Ideas.Any())
        {
            var idea = GetPreconfiguredIdea();

            await dbContext.Ideas.AddAsync(idea, cancellationToken);
            logger.LogInformation("Seeding preconfigured idea");
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

