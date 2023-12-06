using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

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
        DefineActor(modelBuilder.Entity<Actor>());
        DefineGenre(modelBuilder.Entity<Genre>());
        DefineSpecialProp(modelBuilder.Entity<SpecialProp>());
        DefineIdea(modelBuilder.Entity<Idea>());
    }


    private static void DefineIdea(EntityTypeBuilder<Idea> builder)
    {
        builder.ToTable("Idea");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseHiLo("idea_hilo");
        builder.Property(x => x.Plot).IsRequired(false).HasMaxLength(-1);
        builder.Property(x => x.BossReview).IsRequired(false).HasMaxLength(-1);
        builder.Property(x => x.Actors).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Genre).IsRequired().HasMaxLength(50);
        builder.Property(x => x.SpecialProps).IsRequired(false).HasMaxLength(1000);
        builder.Property(x => x.WorkingTitle).IsRequired().HasMaxLength(100);
    }

    private static void DefineActor(EntityTypeBuilder<Actor> builder)
    {
        builder.ToTable("Actor");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseHiLo("actor_hilo");
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
    }

    private static void DefineGenre(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genre");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseHiLo("genre_hilo");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
    private static void DefineSpecialProp(EntityTypeBuilder<SpecialProp> builder)
    {
        builder.ToTable("SpecialProp");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseHiLo("special_prop_hilo");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}

public class Idea
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string WorkingTitle { get; set; }
    public required string Actors { get; set; }
    public required string Genre { get; set; }
    public string SpecialProps { get; set; }
    public string Plot { get; set; }
    public string BossReview { get; set; }
}

public class Actor
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}

public class Genre
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class SpecialProp
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
}
