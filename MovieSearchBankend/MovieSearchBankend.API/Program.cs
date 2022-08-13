// configuring services
var builder = WebApplication.CreateBuilder(args);
{
    builder
        .Services
        .AddEndpointAndServices()
        .AddJsonRequestHandler()
        .AddCorsPolicy()
        .AddApiDocumentation();
}

// configure services
var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.RegisterEndpoints()
        .UseCorsPolicy()
        .UseApiDocumentation();
    
    app.Run();
}