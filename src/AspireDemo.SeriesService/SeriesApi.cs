﻿using AspireDemo.SeriesDb;
using Microsoft.EntityFrameworkCore;

namespace AspireDemo.SeriesService;

public static class SeriesApi
{
    public static RouteGroupBuilder MapStaticDataApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/v1/static-data");

        group.WithTags("Static Data");

        group.MapGet("genres", async (SeriesDbContext db) =>
        {
            var genres = await db.Genres.ToListAsync();
            var retColl = new List<Models.Entities.Genre>();

            foreach (var genre in genres)
            {
                retColl.Add(new Models.Entities.Genre
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }

            return retColl;
        });

        group.MapGet("special-props", async (SeriesDbContext db) =>
        {
            var props = await db.SpecialProps.ToListAsync();
            var retColl = new List<Models.Entities.SpecialProp>();
            foreach (var prop in props)
            {
                retColl.Add(new Models.Entities.SpecialProp
                {
                    Id = prop.Id,
                    Name = prop.Name
                });
            }
            return retColl;
        });

        group.MapGet("actors", async (SeriesDbContext db) =>
        {
            var actors = await db.Actors.ToListAsync();
            var retColl = new List<Models.Entities.Actor>();
            foreach (var actor in actors)
            {
                retColl.Add(new Models.Entities.Actor
                {
                    Id = actor.Id,
                    FirstName = actor.FirstName,
                    LastName = actor.LastName
                });
            }
            return retColl;
        });

        return group;
    }


    public static RouteGroupBuilder MapSeriesApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/v1/series");

        group.WithTags("Series");

        group.MapGet("", async (SeriesDbContext db) => {
            
            var series = await db.Ideas.ToListAsync();
            var retColl = new List<Models.Entities.Idea>();

            foreach (var idea in series)
            {
                retColl.Add(new Models.Entities.Idea
                {
                    Id = idea.Id,
                    WorkingTitle = idea.WorkingTitle,
                    Actors = idea.Actors,
                    Genre = idea.Genre,
                    SpecialProps = idea.SpecialProps,
                    Plot = idea.Plot,
                    GreenlightFromBoss = idea.GreenlightFromBoss,
                    BossNotes = idea.BossNotes
                });
            }

            return retColl;
        });

        group.MapPost("", async (SeriesDbContext db, Models.Entities.Idea idea) =>
        {
            await db.Ideas.AddAsync(new Idea()
            {
                WorkingTitle = idea.WorkingTitle,
                Actors = idea.Actors,
                Genre = idea.Genre,
                SpecialProps = idea.SpecialProps,
            });

            return Results.Created($"/api/v1/series/{idea.Id}", idea);
        });

        group.MapPut("{id:int}/plot", async (int id, SeriesDbContext db, string plot) =>
        {
            var dbIdea = await db.Ideas.FirstOrDefaultAsync(x => x.Id == id);

            if (dbIdea == null)
            {
                return Results.NotFound();
            }

            dbIdea.Plot = plot;
            await db.SaveChangesAsync();
            return Results.Created();
        });

        return group;
    }
}

