namespace Smdb.Core.ActorMovies;

using Shared.Http;
using System.Net;

public class DefaultActorMovieService : IActorMovieService
{
    private readonly IActorMovieRepository repository;

    public DefaultActorMovieService(IActorMovieRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PagedResult<ActorMovie>>> ReadActorMovies(int page, int size)
    {
        if (page < 1 || size < 1)
        {
            return new Result<PagedResult<ActorMovie>>(
                new Exception("Page and size must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actorMovies = await repository.ReadActorMovies(page, size);

        if (actorMovies == null)
        {
            return new Result<PagedResult<ActorMovie>>(
                new Exception("Actors-movies not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<PagedResult<ActorMovie>>(actorMovies);
    }

    public async Task<Result<ActorMovie>> CreateActorMovie(ActorMovie newActorMovie)
    {
        if (newActorMovie.ActorId <= 0)
        {
            return new Result<ActorMovie>(
                new Exception("ActorId must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (newActorMovie.MovieId <= 0)
        {
            return new Result<ActorMovie>(
                new Exception("MovieId must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newActorMovie.Role))
        {
            return new Result<ActorMovie>(
                new Exception("Role is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actorMovie = await repository.CreateActorMovie(newActorMovie);

        if (actorMovie == null)
        {
            return new Result<ActorMovie>(
                new Exception("ActorMovie could not be created."),
                (int)HttpStatusCode.InternalServerError
            );
        }

        return new Result<ActorMovie>(actorMovie, (int)HttpStatusCode.Created);
    }

    public async Task<Result<ActorMovie>> ReadActorMovie(int id)
    {
        if (id <= 0)
        {
            return new Result<ActorMovie>(
                new Exception("ActorMovie id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actorMovie = await repository.ReadActorMovie(id);

        if (actorMovie == null)
        {
            return new Result<ActorMovie>(
                new Exception("ActorMovie not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<ActorMovie>(actorMovie);
    }

    public async Task<Result<ActorMovie>> UpdateActorMovie(int id, ActorMovie newData)
    {
        if (id <= 0)
        {
            return new Result<ActorMovie>(
                new Exception("ActorMovie id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (newData.ActorId <= 0)
        {
            return new Result<ActorMovie>(
                new Exception("ActorId must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (newData.MovieId <= 0)
        {
            return new Result<ActorMovie>(
                new Exception("MovieId must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newData.Role))
        {
            return new Result<ActorMovie>(
                new Exception("Role is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actorMovie = await repository.UpdateActorMovie(id, newData);

        if (actorMovie == null)
        {
            return new Result<ActorMovie>(
                new Exception("ActorMovie not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<ActorMovie>(actorMovie);
    }

    public async Task<Result<ActorMovie>> DeleteActorMovie(int id)
    {
        if (id <= 0)
        {
            return new Result<ActorMovie>(
                new Exception("ActorMovie id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actorMovie = await repository.DeleteActorMovie(id);

        if (actorMovie == null)
        {
            return new Result<ActorMovie>(
                new Exception("ActorMovie not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<ActorMovie>(actorMovie);
    }
}