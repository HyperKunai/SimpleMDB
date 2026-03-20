namespace Smdb.Core.Actors;

using Shared.Http;
using System.Net;

public class DefaultActorService : IActorService
{
    private readonly IActorRepository repository;

    public DefaultActorService(IActorRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PagedResult<Actor>>> ReadActors(int page, int size)
    {
        if (page < 1 || size < 1)
        {
            return new Result<PagedResult<Actor>>(
                new Exception("Page and size must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actors = await repository.ReadActors(page, size);

        if (actors == null)
        {
            return new Result<PagedResult<Actor>>(
                new Exception("Actors not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<PagedResult<Actor>>(actors);
    }

    public async Task<Result<Actor>> CreateActor(Actor newActor)
    {
        if (string.IsNullOrWhiteSpace(newActor.Name))
        {
            return new Result<Actor>(
                new Exception("Actor name is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (newActor.Age <= 0)
        {
            return new Result<Actor>(
                new Exception("Actor age must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actor = await repository.CreateActor(newActor);

        if (actor == null)
        {
            return new Result<Actor>(
                new Exception("Actor could not be created."),
                (int)HttpStatusCode.InternalServerError
            );
        }

        return new Result<Actor>(actor, (int)HttpStatusCode.Created);
    }

    public async Task<Result<Actor>> ReadActor(int id)
    {
        if (id <= 0)
        {
            return new Result<Actor>(
                new Exception("Actor id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actor = await repository.ReadActor(id);

        if (actor == null)
        {
            return new Result<Actor>(
                new Exception("Actor not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<Actor>(actor);
    }

    public async Task<Result<Actor>> UpdateActor(int id, Actor newData)
    {
        if (id <= 0)
        {
            return new Result<Actor>(
                new Exception("Actor id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newData.Name))
        {
            return new Result<Actor>(
                new Exception("Actor name is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (newData.Age <= 0)
        {
            return new Result<Actor>(
                new Exception("Actor age must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actor = await repository.UpdateActor(id, newData);

        if (actor == null)
        {
            return new Result<Actor>(
                new Exception("Actor not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<Actor>(actor);
    }

    public async Task<Result<Actor>> DeleteActor(int id)
    {
        if (id <= 0)
        {
            return new Result<Actor>(
                new Exception("Actor id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var actor = await repository.DeleteActor(id);

        if (actor == null)
        {
            return new Result<Actor>(
                new Exception("Actor not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<Actor>(actor);
    }
}