using MovieSearchBankend.API.Infrastructures.Handlers;
using MovieSearchBankend.API.Responses;

namespace MovieSearchBankend.API.Services;

public class MovieSearchService : IMovieSearchService
{
    private readonly JsonRequest _jsonRequest;
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;
    public MovieSearchService(JsonRequest jsonRequest, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(jsonRequest, nameof(jsonRequest));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        _jsonRequest = jsonRequest;
        _configuration = configuration;
        _baseUrl = configuration.GetValue<string>("omdbapi");
    }

    public async Task<Movie> SearchByIdAsync(string id)
    {
        var response = await _jsonRequest
                .SetUrl($"{_baseUrl}i={id}")
                .SetRequestType(JsonRequest.RequestType.GET)
                .Execute();

        if (response.StatusCode != HttpStatusCode.OK)

            return new Movie { Error = response.Data };

        if (response is not null && response.Data is not null)
            return JsonSerializer.Deserialize<Movie>(response.Data)!;

        return new Movie { Error = "Movie not found!" };
    }

    public async Task<Movie> SearchByTitleAsync(string title)
    {
        var response = await _jsonRequest
                .SetUrl($"{_baseUrl}t={title}")
                .SetRequestType(JsonRequest.RequestType.GET)
                .Execute();

        if (response.StatusCode != HttpStatusCode.OK)

            return new Movie { Error = response.Data };

        if (response is not null && response.Data is not null)
            return JsonSerializer.Deserialize<Movie>(response.Data)!;

        return new Movie { Error = "Movie not found!" };
    }
}
