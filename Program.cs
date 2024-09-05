class Movie
{
  public string Title {get; set;}

  public Movie(string title)
  {
    Title = title;
  }
}


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //Register the list with Depenency Injection Container
        builder.Services.AddSingleton<List<Movie>>();
        var app = builder.Build();

        // READ: Get all movies
        app.MapGet("/movies", (List<Movie> movies) => movies);
        // CREATE: Adds a new movie
        app.MapPost("/movies", (Movie? movie, List<Movie> movies) => 
        {
          if (movie == null)
          {
            return Results.BadRequest();
          }
          movies.Add(movie);
          return Results.Created();
        });

        // UPDATE: Update a movie
        app.MapPut("/movies/{Id}", (int Id) => $"Update movie with id: {Id}");
        // DELETE: Delete a movie
        app.MapDelete("/movies/{Id}", (int Id) => $"Delete movie with id: {Id}");


        // Check system health
        app.MapGet("/health", () =>
        {
            return "System healthy";
        });



        app.Run();
    }
}