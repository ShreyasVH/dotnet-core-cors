using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine();
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddControllers();
                    services.AddCors(options =>
                    {
                        options.AddDefaultPolicy(
                            policy  =>
                            {
                                policy
                                    .WithOrigins((Environment.GetEnvironmentVariable("ASPNETCORE_ALLOWED_ORIGINS") ?? "").Split(","))
                                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                                    .WithExposedHeaders("access-control-allow-headers", "access-control-allow-origin", "access-control-allow-methods")
                                    .WithHeaders("Accept", "Origin", "X-Requested-With", "Content-Type", "Referer", "User-Agent", "Access-Control-Allow-Origin");
                            });
                    });
                });
                webBuilder.Configure(app =>
                {
                    app.UseRouting();
                    app.UseCors();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                        
                        // endpoints.MapMethods("/api", new[] { "OPTIONS" }, context =>
                        // {
                        //     context.Response.Headers.Add("Allow", "OPTIONS,GET,POST,PUT,DELETE");
                        //     context.Response.StatusCode = 200;
                        //     return Task.CompletedTask;
                        // });
                    });
                    
                    // app.Use((context, next) =>
                    // {
                    //     if (context.Request.Method == "OPTIONS")
                    //     {
                    //         context.Response.Headers.Add("Allow", "OPTIONS,GET,POST,PUT,DELETE");
                    //         context.Response.StatusCode = 200;
                    //         return Task.CompletedTask;
                    //     }
                    //
                    //     return next();
                    // });
                });
                // webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://localhost:" + GetPortFromEnvironment());
            });

    private static string GetPortFromEnvironment()
    {
        // Read the environment variable for the port, e.g., "ASPNETCORE_PORT"
        string port = Environment.GetEnvironmentVariable("ASPNETCORE_PORT") ?? "5000";
        return port;
    }
}