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
                    });
                });
                // webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://localhost:" + GetPortFromEnvironment());
            });

    private static string GetPortFromEnvironment()
    {
        return Environment.GetEnvironmentVariable("PORT") ?? "";
    }
}