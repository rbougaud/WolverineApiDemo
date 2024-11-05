using WolverineApiDemo.Helper;
using FluentValidation;

namespace WolverineApiDemo.Movies.Interfaces;

public interface IMovieService
{
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(Guid id);
    Task<Result<Movie, ValidationException>> CreateAsync(Movie movie);
}
