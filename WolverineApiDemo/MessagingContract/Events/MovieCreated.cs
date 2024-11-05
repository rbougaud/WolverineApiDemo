namespace MessagingContract.Events;

public record MovieCreated
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required int YearOfReleased { get; init; }
}
