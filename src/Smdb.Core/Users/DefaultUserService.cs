namespace Smdb.Core.Users;

using Shared.Http;
using System.Net;

public class DefaultUserService : IUserService
{
    private readonly IUserRepository repository;

    public DefaultUserService(IUserRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PagedResult<User>>> ReadUsers(int page, int size)
    {
        if (page < 1 || size < 1)
        {
            return new Result<PagedResult<User>>(
                new Exception("Page and size must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var users = await repository.ReadUsers(page, size);

        if (users == null)
        {
            return new Result<PagedResult<User>>(
                new Exception("Users not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<PagedResult<User>>(users);
    }

    public async Task<Result<User>> CreateUser(User newUser)
    {
        if (string.IsNullOrWhiteSpace(newUser.Username))
        {
            return new Result<User>(
                new Exception("Username is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newUser.Password))
        {
            return new Result<User>(
                new Exception("Password is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newUser.Role))
        {
            return new Result<User>(
                new Exception("Role is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var user = await repository.CreateUser(newUser);

        if (user == null)
        {
            return new Result<User>(
                new Exception("User could not be created."),
                (int)HttpStatusCode.InternalServerError
            );
        }

        return new Result<User>(user, (int)HttpStatusCode.Created);
    }

    public async Task<Result<User>> ReadUser(int id)
    {
        if (id <= 0)
        {
            return new Result<User>(
                new Exception("User id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var user = await repository.ReadUser(id);

        if (user == null)
        {
            return new Result<User>(
                new Exception("User not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<User>(user);
    }

    public async Task<Result<User>> UpdateUser(int id, User newData)
    {
        if (id <= 0)
        {
            return new Result<User>(
                new Exception("User id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newData.Username))
        {
            return new Result<User>(
                new Exception("Username is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newData.Password))
        {
            return new Result<User>(
                new Exception("Password is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newData.Role))
        {
            return new Result<User>(
                new Exception("Role is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var user = await repository.UpdateUser(id, newData);

        if (user == null)
        {
            return new Result<User>(
                new Exception("User not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<User>(user);
    }

    public async Task<Result<User>> DeleteUser(int id)
    {
        if (id <= 0)
        {
            return new Result<User>(
                new Exception("User id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var user = await repository.DeleteUser(id);

        if (user == null)
        {
            return new Result<User>(
                new Exception("User not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<User>(user);
    }
}