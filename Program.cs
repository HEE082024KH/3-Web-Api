class Movie
{
  private static int _id = 0;
  public int Id { get; set; }
  public string Title { get; set; }

  public Movie(string title)
  {
    Title = title;
    Id = _id++;
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
        // app.MapPut("/movies/{Id}", (int Id, List<Movie> movies) => 
        // {
        //   var movie = movies.Find{(movie) => movie.Id == Id;

        //   if 
        // });

        // DELETE: Delete a movie
        app.MapDelete("/movies/{Id}", (int Id, List<Movie> movies) => 
        {
          var movie = movies.Find((movie) => movie.Id == Id);

          if (movie == null)
          {
            return Results.NotFound();
          }
          movies.Remove(movie);
          return Results.Ok();
        });


        // Check system health
        app.MapGet("/health", () =>
        {
            return "System healthy";
        });



        app.Run();
    }
}