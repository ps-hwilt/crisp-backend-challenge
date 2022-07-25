using CRISP.Backend.Challenge;
using CRISP.Backend.Challenge.Context;
using Microsoft.AspNetCore;

public static class Program
{
    public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
}