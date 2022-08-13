using Microsoft.AspNetCore.Diagnostics;

namespace MovieSearchBankend.API.Infrastructures.Middlewares;

public static class ExceptionMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    string errorMessage = (env.IsDevelopment() ? contextFeature.Error.GetDetails() : "An unknown error occurred.");

                    if (logger != null)
                    {
                        logger.LogError($"Exception occurred on {DateTime.Now:F}{Environment.NewLine}{contextFeature.Error.GetDetails()}");
                    }

                    await context.Response.WriteAsync(new 
                    {
                        hasError = true,
                        error = errorMessage
                    }.ToString());
                }
            });
        });
    }
}
