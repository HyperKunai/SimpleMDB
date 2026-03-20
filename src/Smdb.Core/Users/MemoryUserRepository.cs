namespace Smdb.Core.Users;

using Shared.Http;
using Smdb.Core.Db;

public class MemoryUserRepository : IUserRepository
{
    private readonly MemoryDatabase db;

    public MemoryUserRepository(MemoryDatabase db)
    {
        this.db = db;
    }

    public Task<PagedResult<User>?> ReadUsers(int page, int size)
    {
        if (page < 1 || size < 1)
        {
            return Task.FromResult<PagedResult<User>?>(null);
        }

        int start = (page - 1) * size;

        var users = db.Users
            .Skip(start)
            .Take(size)
            .ToList();

        var result = new PagedResult<User>(db.Users.Count, users);

        return Task.FromResult<PagedResult<User>?>(result);
    }

    public Task<User?> CreateUser(User newUser)
    {
        var user = new User(
            db.NextUserId(),
            newUser.Username,
            newUser.Password,
            newUser.Role
        );

        db.Users.Add(user);
        return Task.FromResult<User?>(user);
    }

    public Task<User?> ReadUser(int id)
    {
        var user = db.Users.FirstOrDefault(u => u.Id == id);
        return Task.FromResult<User?>(user);
    }

    public async Task<User?> UpdateUser(int id, User newData)
    {
        var user = await ReadUser(id);

        if (user == null)
        {
            return null;
        }

        user.Username = newData.Username;
        user.Password = newData.Password;
        user.Role = newData.Role;

        return user;
    }

    public async Task<User?> DeleteUser(int id)
    {
        var user = await ReadUser(id);

        if (user == null)
        {
            return null;
        }

        db.Users.Remove(user);
        return user;
    }
}