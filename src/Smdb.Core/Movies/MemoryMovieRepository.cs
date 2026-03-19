namespace Smdb.Core.Movies;

using Shared.Http;
using Smdb.Core.Db;

public class MemoryMovieRepository : IMovieRepository
{
    private readonly MemoryDatabase db;

    public MemoryMovieRepository(MemoryDatabase db)
    {
        this.db = db;
    }

    public Task<PagedResult<Movie>?> ReadMovies(int page, int size)
    {
        if (page < 1 || size < 1)
        {
            return Task.FromResult<PagedResult<Movie>?>(null);
        }

        int start = (page - 1) * size;

        var movies = db.Movies
            .Skip(start)
            .Take(size)
            .ToList();

        var result = new PagedResult<Movie>(db.Movies.Count, movies);

        return Task.FromResult<PagedResult<Movie>?>(result);
    }

    public Task<Movie?> CreateMovie(Movie newMovie)
    {
        var movie = new Movie(
            db.NextMovieId(),
            newMovie.Title,
            newMovie.Year,
            newMovie.Description
        );

        db.Movies.Add(movie);

        return Task.FromResult<Movie?>(movie);
    }

    public Task<Movie?> ReadMovie(int id)
    {
        var movie = db.Movies.FirstOrDefault(m => m.Id == id);
        return Task.FromResult<Movie?>(movie);
    }

    public async Task<Movie?> UpdateMovie(int id, Movie newData)
    {
        var movie = await ReadMovie(id);

        if (movie == null)
        {
            return null;
        }

        movie.Title = newData.Title;
        movie.Year = newData.Year;
        movie.Description = newData.Description;

        return movie;
    }

    public async Task<Movie?> DeleteMovie(int id)
    {
        var movie = await ReadMovie(id);

        if (movie == null)
        {
            return null;
        }

        db.Movies.Remove(movie);
        return movie;
    }
}