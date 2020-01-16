using Orleans;
using Orleans.Configuration;
using System.Threading.Tasks;

namespace Client_WebMVCApp.Services
{
    public class OrleansService : IOrleansService
    {
        private IClusterClient clusterClient;

        public OrleansService()
        {
            clusterClient = ConnectClient().Result;
        }

        public T GetGrain<T>(long integerKey) where T : IGrainWithIntegerKey
        {
            return clusterClient.GetGrain<T>(integerKey);
        }

        /// <summary>
        /// 使用本地配置连接服务
        /// </summary>
        /// <returns></returns>
        private async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()           //配置客户端以连接到本地主机上的筒仓。
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MyHost";
                })
                //.UseStaticClustering(new IPEndPoint[] { new IPEndPoint(IPAddress.Parse(""), 30000) })
                .Build();
            await client.Connect();
            return client;
        }
    }
}
