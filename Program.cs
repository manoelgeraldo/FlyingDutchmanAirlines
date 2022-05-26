using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        InitializeHost();
    }

    private static void InitializeHost() => 
        Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder =>{
            builder.UseStartup<Startup>();
            builder.UseUrls("http://0.0.0.0:8080");
        }).Build().Run();
}