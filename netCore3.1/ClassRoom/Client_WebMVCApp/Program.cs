using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using Orleans.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Client_WebMVCApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseOrleans(siloBuilder =>
                {
                    siloBuilder
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(opts =>
                    {
                        opts.ClusterId = "dev";
                        opts.ServiceId = "MyHost";
                    })
                    .Configure<EndpointOptions>(opts =>
                    {
                        opts.AdvertisedIPAddress = IPAddress.Loopback;
                    });
                });



        public static IHostBuilder CreateHostBuilder2(string[] args) =>
            (IHostBuilder)Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.Configure((ctx, app) =>
                    //{
                    //    if (ctx.HostingEnvironment.IsDevelopment())
                    //    {
                    //        app.UseDeveloperExceptionPage();
                    //    }

                    //    app.UseHttpsRedirection();
                    //    app.UseRouting();
                    //    app.UseAuthorization();
                    //    app.UseEndpoints(endpoints =>
                    //    {
                    //        endpoints.MapControllers();
                    //    });
                    //});
                })
                .ConfigureServices(services =>
                {
                    services.AddControllers();
                })
                .UseOrleans(siloBuilder =>
                {
                    siloBuilder
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(opts =>
                    {
                        opts.ClusterId = "dev";
                        opts.ServiceId = "MyHost";
                    })
                    .Configure<EndpointOptions>(opts =>
                    {
                        opts.AdvertisedIPAddress = IPAddress.Loopback;
                    });
                })
            .RunConsoleAsync();
    }
}
