namespace Smdb.Core.Actors;

using Shared.Http;
using Smdb.Core.Db;

public class MemoryActorRepository : IActorRepository
{
    private readonly MemoryDatabase db;

    public MemoryActorRepository(MemoryDatabase db)
    {
        this.db = db;
    }

    public Task<PagedResult<Actor>?> ReadActors(int page, int size)
    {
        if (page < 1 || size < 1)
        {
            return Task.FromResult<PagedResult<Actor>?>(null);
        }

        int start = (page - 1) * size;

        var actors = db.Actors
            .Skip(start)
            .Take(size)
            .ToList();

        var result = new PagedResult<Actor>(db.Actors.Count, actors);

        return Task.FromResult<PagedResult<Actor>?>(result);
    }

    public Task<Actor?> CreateActor(Actor newActor)
    {
        var actor = new Actor(
            db.NextActorId(),
            newActor.Name,
            newActor.Age,
            newActor.Bio
        );

        db.Actors.Add(actor);
        return Task.FromResult<Actor?>(actor);
    }

    public Task<Actor?> ReadActor(int id)
    {
        var actor = db.Actors.FirstOrDefault(a => a.Id == id);
        return Task.FromResult<Actor?>(actor);
    }

    public async Task<Actor?> UpdateActor(int id, Actor newData)
    {
        var actor = await ReadActor(id);

        if (actor == null)
        {
            return null;
        }

        actor.Name = newData.Name;
        actor.Age = newData.Age;
        actor.Bio = newData.Bio;

        return actor;
    }

    public async Task<Actor?> DeleteActor(int id)
    {
        var actor = await ReadActor(id);

        if (actor == null)
        {
            return null;
        }

        db.Actors.Remove(actor);
        return actor;
    }
}