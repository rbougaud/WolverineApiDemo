using FluentValidation;
using MessagingContract.Events;
using System.Collections.Concurrent;
using Wolverine;
using WolverineApiDemo.Helper;
using WolverineApiDemo.Movies.Interfaces;

namespace WolverineApiDemo.Movies;

public class MovieService(IValidator<Movie> validator, IMessageBus messageBus) : IMovieService
{
    private readonly IValidator<Movie> _validator = validator;
    private readonly IMessageBus _messageBus = messageBus;
    private readonly ConcurrentDictionary<Guid, Movie> _movies = new();

    public async Task<Result<Movie, ValidationException>> CreateAsync(Movie movie)
    {
        var validationResult = await _validator.ValidateAsync(movie);
        if (!validationResult.IsValid)
        {
            return new ValidationException(validationResult.Errors);
        }

        _movies[movie.Id] = movie;

        MovieCreated message = movie.MapToCreated();
        //TODO RBO retry mecanism / but if it fails, a trigger in database will do the retry
        await _messageBus.PublishAsync(message); //SendAsync if we want an object result
        return movie;
    }

    public Task<IEnumerable<Movie>> GetAllAsync()
    {
        return Task.FromResult(_movies.Values.AsEnumerable());
    }

    public Task<Movie?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_movies.GetValueOrDefault(id));
    }
}
