using MessagingContract.Events;

namespace MoviesConsumerApi.Handlers;

public class MovieCreatedHandler(ILogger<MovieCreatedHandler> logger)
{
    private readonly ILogger<MovieCreatedHandler> _logger = logger;

    public void Handle(MovieCreated movieCreated)
    {
        _logger.LogInformation(movieCreated.ToString());
    }
}
