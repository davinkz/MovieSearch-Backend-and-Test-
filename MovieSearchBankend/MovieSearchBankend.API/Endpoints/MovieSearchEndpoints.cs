namespace MovieSearchBankend.API.Endpoints;

public class MovieSearchEndpoints : IEndpointBase
{
    private WebApplication _app;
    private readonly ILogger<MovieSearchEndpoints> _logger;
    private readonly IMovieSearchService _movieSearchService;

    public MovieSearchEndpoints(ILogger<MovieSearchEndpoints> logger, IMovieSearchService movieSearchService)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(movieSearchService);

        _logger = logger;
        _movieSearchService = movieSearchService;
    }

    public void MapEndpoints(WebApplication app)
    {
        _app = app;
        app.MapGet("/api/movie/search-by-title/{title}", SearchMovieByTitleAsync);
        app.MapGet("/api/movie/search-by-id/{id}", SearchMovieByIDAsync);
    }

    private async Task<IResult> SearchMovieByTitleAsync(string title, IMovieSearchService movieSearchService)
    {
        Movie movie = new();
        try
        {
            movie = await movieSearchService.SearchByTitleAsync(title);
        }
        catch (Exception ex)
        {
            movie.Error = ex.GetDetails();           
        }
        return Results.Ok(movie);
    }

    private async Task<IResult> SearchMovieByIDAsync(string id, IMovieSearchService movieSearchService)
    {
        Movie movie = new();
        try
        {
            movie = await movieSearchService.SearchByIdAsync(id);
        }
        catch (Exception ex)
        {
            movie.Error = ex.GetDetails();           
        }
        return Results.Ok(movie);
    }
}

