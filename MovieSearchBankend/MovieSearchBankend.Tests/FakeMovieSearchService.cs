
using MovieSearchBankend.API.Interfaces;
using MovieSearchBankend.API.Responses;

namespace MovieSearchBankend.Tests;

internal class FakeMovieSearchService : IMovieSearchService
{
    public async Task<Movie> SearchByIdAsync(string id)
    {
        var movieNotFound = "Movie not found!";
        var movies = await FakeDataProvider.GetSampleMoviesAsync();
        if (movies is null)
            return new Movie { Error = movieNotFound };

        var movie = movies.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.imdbID) && x.Title.ToLower().Contains(id.ToLower()));
        if (movie is null)
            return new Movie { Error = movieNotFound };

        return movie;
    }

    public async Task<Movie> SearchByTitleAsync(string title)
    {
        var movieNotFound = "Movie not found!";
        var movies = await FakeDataProvider.GetSampleMoviesAsync();
        if (movies is null)
            return new Movie { Error = movieNotFound };

        var movie = movies.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Title) && x.Title.ToLower().Contains(title.ToLower()));
        if (movie is null)
            return new Movie { Error = movieNotFound };

        return movie;
    }
}