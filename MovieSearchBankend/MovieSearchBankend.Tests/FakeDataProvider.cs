using MovieSearchBankend.API.Responses;

using System.Text.Json;

namespace MovieSearchBankend.Tests;

internal class FakeDataProvider
{
    public static async Task<IEnumerable<Movie>> GetSampleMoviesAsync()
    {
        var filePath = Path.Combine("SampleData", "movies.json");
        using var stream = new StreamReader(filePath);
        var jsonString = await stream.ReadToEndAsync();

        return JsonSerializer.Deserialize<IEnumerable<Movie>>(jsonString)!;
    }
}
