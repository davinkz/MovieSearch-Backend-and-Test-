namespace MovieSearchBankend.API.Infrastructures.Middlewares;

public static class DependencyInjectionMiddleware
{
    private const string CORS_POLICY_NAME = "MovieSearchCors";

    //************************ add services *************************
    public static IServiceCollection AddEndpointAndServices(this IServiceCollection services)
    {
        services.AddTransient<IEndpointBase, MovieSearchEndpoints>();
        services.AddTransient<IMovieSearchService, MovieSearchService>();
        return services;
    }

    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: CORS_POLICY_NAME,
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    }
                );
        });
        return services;
    }

    public static IServiceCollection AddJsonRequestHandler(this IServiceCollection services)
    {
        services.AddTransient<JsonRequest>();
        return services;
    }

    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }


    //************************ configure services *************************
    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        var apis = app.Services.GetServices<IEndpointBase>();
        foreach (var api in apis)
        {
            if (api is null) throw new InvalidProgramException($"Api {nameof(api)} not found.");

            api.MapEndpoints(app);
        }
        return app;
    }

    public static WebApplication UseCorsPolicy(this WebApplication app)
    {
        app.UseCors(CORS_POLICY_NAME);
        return app;
    }

    public static WebApplication UseApiDocumentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"./swagger/v1/swagger.json", $"Movie Search API v1");
            options.RoutePrefix = string.Empty;
        });
        return app;
    }
}