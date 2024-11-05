using FluentValidation;
using Wolverine;
using WolverineApiDemo.Movies.Interfaces;
using WolverineApiDemo.Movies;
using Wolverine.RabbitMQ;
using MessagingContract.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(x =>
{
    x.PublishAllMessages().ToRabbitExchange("movies-exc", exchange =>
    {
        exchange.ExchangeType = ExchangeType.Direct;
        exchange.BindQueue("movies-queue", "exchange2movies");
    });
    x.UseRabbitMq(c =>
    {
        c.HostName = "localhost";
    }).AutoProvision();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.MapPost("movies", async (
    CreateMovieRequest request, IMovieService movieService) =>
{
    var movie = request.MapToMovie();
    var result = await movieService.CreateAsync(movie);
    return Guid.TryParse(movie.Id.ToString(), out Guid movieId) ? await movieService.GetByIdAsync(movieId) : null;
});

app.MapGet("movies/{id}", async (
    string id, IMovieService movieService) =>
{
    return Guid.TryParse(id, out Guid movieId) ? await movieService.GetByIdAsync(movieId) : null;
});
app.UseHttpsRedirection();
app.Run();
