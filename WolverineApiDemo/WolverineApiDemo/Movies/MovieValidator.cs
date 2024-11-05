using FluentValidation;

namespace WolverineApiDemo.Movies;

public class MovieValidator : AbstractValidator<Movie>
{
    public MovieValidator()
    {
        RuleFor(x => x.Title).NotNull();
        RuleFor(x => x.Author).NotEmpty();
    }
}
