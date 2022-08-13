using System.Net;

namespace MovieSearchBankend.Tests
{
    public class MovieSearchTests
    {
        [Theory]
        [InlineData("gala")]
        public async Task TestSearchMovieByTitle(string title)
        {
            await using var application = new FakeMovieSearchApplication();
            using var client = application.CreateClient();

            var result = await client.GetAsync($"api/movie/search-by-title/{title}");

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result);
        }
    }
}
