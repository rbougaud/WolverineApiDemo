using MessagingContract.Events;
using MessagingContract.Requests;

namespace WolverineApiDemo.Movies;

public static class MoviesMapper
{
    public static MovieCreated MapToCreated(this Movie movie)
    {
        return new MovieCreated
        {
            Id = movie.Id,
            Author = movie.Author,
            Title = movie.Title,
            YearOfReleased = movie.YearOfReleased
        };
    }

    public static Movie MapToMovie(this CreateMovieRequest request)
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Author = request.Author,
            Title = request.Title,
            YearOfReleased = request.YearOfReleased
        };
    }
}
