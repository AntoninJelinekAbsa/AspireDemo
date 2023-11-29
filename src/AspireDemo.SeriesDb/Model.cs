using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspireDemo.SeriesDb;

public record Ideas(int FirstId, int NextId, bool IsLastPage, IEnumerable<Idea> Data);

public class SeriesDbContext(DbContextOptions<SeriesDbContext> options) : DbContext(options)
{
    public DbSet<Idea> Ideas => Set<Idea>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<SpecialProp> SpecialProps => Set<SpecialProp>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DefineIdea(modelBuilder.Entity<Idea>());
        DefineActor(modelBuilder.Entity<Actor>());
        DefineGenre(modelBuilder.Entity<Genre>());
        DefineSpecialProp(modelBuilder.Entity<SpecialProp>());
    }


    private static void DefineIdea(EntityTypeBuilder<Idea> builder)
    {
        builder.ToTable("Idea");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseHiLo("idea_hilo").IsRequired();
        builder.Property(x => x.Plot).IsRequired();
        builder.Property(x => x.GreenlightFromBoss).IsRequired();
        builder.Property(x => x.BossNotes).IsRequired(false);
        builder.HasMany(x => x.Actors).WithOne().IsRequired();
        builder.HasOne(x => x.Genre).WithOne().IsRequired();
        builder.HasMany(x => x.SpecialProps).WithOne().IsRequired(false);
    }

    private static void DefineActor(EntityTypeBuilder<Actor> builder)
    {
        builder.ToTable("Actor");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseHiLo("actor_hilo").IsRequired();
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
    }

    private static void DefineGenre(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genre");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseHiLo("genre_hilo").IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
    private static void DefineSpecialProp(EntityTypeBuilder<SpecialProp> builder)
    {
        builder.ToTable("SpecialProp");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseHiLo("special_prop_hilo").IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}

public class Idea
{
    public int Id { get; set; }
    public required List<Actor> Actors { get; set; }
    public required Genre Genre { get; set; }
    public List<SpecialProp> SpecialProps { get; set; }

    public string Plot { get; set; }
    public bool GreenlightFromBoss { get; set; }
    public string BossNotes { get; set; }
}

public class Actor
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class SpecialProp
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
