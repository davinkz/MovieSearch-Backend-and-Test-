using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MovieSearchBankend.API.Interfaces;
using MovieSearchBankend.API.Responses;

namespace MovieSearchBankend.Tests;

internal class FakeMovieSearchApplication: WebApplicationFactory<Movie>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddTransient<IMovieSearchService, FakeMovieSearchService>();
        });
        return base.CreateHost(builder);
    }
}