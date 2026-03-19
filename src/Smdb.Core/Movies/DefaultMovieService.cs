namespace Smdb.Core.Movies;

using Shared.Http;
using System.Net;

public class DefaultMovieService : IMovieService
{
    private readonly IMovieRepository repository;

    public DefaultMovieService(IMovieRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PagedResult<Movie>>> ReadMovies(int page, int size)
    {
        if (page < 1 || size < 1)
        {
            return new Result<PagedResult<Movie>>(
                new Exception("Page and size must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var movies = await repository.ReadMovies(page, size);

        if (movies == null)
        {
            return new Result<PagedResult<Movie>>(
                new Exception("Movies not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<PagedResult<Movie>>(movies);
    }

    public async Task<Result<Movie>> CreateMovie(Movie newMovie)
    {
        if (string.IsNullOrWhiteSpace(newMovie.Title))
        {
            return new Result<Movie>(
                new Exception("Movie title is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (newMovie.Year <= 0)
        {
            return new Result<Movie>(
                new Exception("Movie year must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var movie = await repository.CreateMovie(newMovie);

        if (movie == null)
        {
            return new Result<Movie>(
                new Exception("Movie could not be created."),
                (int)HttpStatusCode.InternalServerError
            );
        }

        return new Result<Movie>(movie, (int)HttpStatusCode.Created);
    }

    public async Task<Result<Movie>> ReadMovie(int id)
    {
        if (id <= 0)
        {
            return new Result<Movie>(
                new Exception("Movie id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var movie = await repository.ReadMovie(id);

        if (movie == null)
        {
            return new Result<Movie>(
                new Exception("Movie not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<Movie>(movie);
    }

    public async Task<Result<Movie>> UpdateMovie(int id, Movie newData)
    {
        if (id <= 0)
        {
            return new Result<Movie>(
                new Exception("Movie id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (string.IsNullOrWhiteSpace(newData.Title))
        {
            return new Result<Movie>(
                new Exception("Movie title is required."),
                (int)HttpStatusCode.BadRequest
            );
        }

        if (newData.Year <= 0)
        {
            return new Result<Movie>(
                new Exception("Movie year must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var movie = await repository.UpdateMovie(id, newData);

        if (movie == null)
        {
            return new Result<Movie>(
                new Exception("Movie not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<Movie>(movie);
    }

    public async Task<Result<Movie>> DeleteMovie(int id)
    {
        if (id <= 0)
        {
            return new Result<Movie>(
                new Exception("Movie id must be greater than 0."),
                (int)HttpStatusCode.BadRequest
            );
        }

        var movie = await repository.DeleteMovie(id);

        if (movie == null)
        {
            return new Result<Movie>(
                new Exception("Movie not found."),
                (int)HttpStatusCode.NotFound
            );
        }

        return new Result<Movie>(movie);
    }
}