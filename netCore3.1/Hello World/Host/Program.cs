using Grain;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Host
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("\n\n 按回车键停止 \n\n");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            //定义群集配置
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()//配置Silo只使用开发集群，并监听本地主机
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";//获取或设置群集标识。这在Orleans 2.0名称之前曾被称为DeploymentId。
                    options.ServiceId = "OrleansBasics";//获取或设置此服务的唯一标识符，该标识符应在部署和重新部署后继续存在，其中Orleans.Configuration.ClusterOptions.ClusterId可能不存在。
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();//运行给定的配置来初始化主机。只能调用一次。
            await host.StartAsync();//启动当前Silo.
            return host;
        }
    }
}
