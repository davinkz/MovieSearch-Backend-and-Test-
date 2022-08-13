namespace MovieSearchBankend.API.Interfaces;

public interface IMovieSearchService
{
    Task<Movie> SearchByTitleAsync(string title);
    Task<Movie> SearchByIdAsync(string id);
}