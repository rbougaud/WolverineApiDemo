namespace WolverineApiDemo.Movies;

public class Movie
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public int YearOfReleased { get; set; }
}
