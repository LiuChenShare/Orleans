using IGrains;
using Orleans;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;

namespace Client_ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("开始启动Silo!");
            try
            {
                using (var client = await ConnectClient())
                {
                    Console.WriteLine("客户端已成功连接到Silo Host  \n");
                    await DoClientWork(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("\n\n 按任意键退出 \n\n");
            Console.ReadKey();
            return;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            //从客户端调用Grain的示例
            var student = client.GetGrain<IStudent>(321);
            var response = await student.SayHello();
            Console.WriteLine("\n\n{0}\n\n", response);
        }

        /// <summary>
        /// 使用本地配置连接服务
        /// </summary>
        /// <returns></returns>
        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()           //配置客户端以连接到本地主机上的筒仓。
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MyHost";
                })
                .Build();
            await client.Connect();
            return client;
        }
    }
}
