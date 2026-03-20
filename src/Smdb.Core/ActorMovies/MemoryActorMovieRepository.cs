namespace Smdb.Core.ActorMovies;

using Shared.Http;
using Smdb.Core.Db;

public class MemoryActorMovieRepository : IActorMovieRepository
{
    private readonly MemoryDatabase db;

    public MemoryActorMovieRepository(MemoryDatabase db)
    {
        this.db = db;
    }

    public Task<PagedResult<ActorMovie>?> ReadActorMovies(int page, int size)
    {
        if (page < 1 || size < 1)
        {
            return Task.FromResult<PagedResult<ActorMovie>?>(null);
        }

        int start = (page - 1) * size;

        var actorMovies = db.ActorMovies
            .Skip(start)
            .Take(size)
            .ToList();

        var result = new PagedResult<ActorMovie>(db.ActorMovies.Count, actorMovies);

        return Task.FromResult<PagedResult<ActorMovie>?>(result);
    }

    public Task<ActorMovie?> CreateActorMovie(ActorMovie newActorMovie)
    {
        var actorMovie = new ActorMovie(
            db.NextActorMovieId(),
            newActorMovie.ActorId,
            newActorMovie.MovieId,
            newActorMovie.Role
        );

        db.ActorMovies.Add(actorMovie);
        return Task.FromResult<ActorMovie?>(actorMovie);
    }

    public Task<ActorMovie?> ReadActorMovie(int id)
    {
        var actorMovie = db.ActorMovies.FirstOrDefault(am => am.Id == id);
        return Task.FromResult<ActorMovie?>(actorMovie);
    }

    public async Task<ActorMovie?> UpdateActorMovie(int id, ActorMovie newData)
    {
        var actorMovie = await ReadActorMovie(id);

        if (actorMovie == null)
        {
            return null;
        }

        actorMovie.ActorId = newData.ActorId;
        actorMovie.MovieId = newData.MovieId;
        actorMovie.Role = newData.Role;

        return actorMovie;
    }

    public async Task<ActorMovie?> DeleteActorMovie(int id)
    {
        var actorMovie = await ReadActorMovie(id);

        if (actorMovie == null)
        {
            return null;
        }

        db.ActorMovies.Remove(actorMovie);
        return actorMovie;
    }
}