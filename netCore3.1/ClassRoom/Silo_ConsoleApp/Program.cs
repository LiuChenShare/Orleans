using Grains;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Silo_ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("开始启动Silo!");
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Silo启动完成");
                Console.WriteLine("\n\n 按回车键停止 \n\n");
                Console.ReadLine();
                await host.StopAsync();//停止当前Silo
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }
        }

        /// <summary>
        /// 启动本地配置
        /// </summary>
        /// <returns></returns>
        private static async Task<ISiloHost> StartSilo()
        {
            var host = new SiloHostBuilder()
                 .UseLocalhostClustering()                     //配置Silo只使用开发集群，并监听本地主机。
                 .Configure<ClusterOptions>(options =>
                 {
                     options.ClusterId = "dev";
                     options.ServiceId = "MyHost";             //获取或设置此服务的唯一标识符，该标识符应在部署和重新部署后仍然有效
                 })
                 .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)//配置Silo的端口
                 .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(Student).Assembly).WithReferences())
                 .Build();
            await host.StartAsync();//启动当前Silo.
            return host;
        }
    }
}
