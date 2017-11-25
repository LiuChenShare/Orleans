using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Runtime.Host;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            //获得一个配置实例
            //他需要两个端口，第一个端口2334是用来silo与silo之间的通信的，第二个1234是用于监听client的请求的
            var config = Orleans.Runtime.Configuration.ClusterConfiguration.LocalhostPrimarySilo(2234, 1234);
            //初始化一个silohost,这里使用了Orleans提供的silohost而不是silo，其中silo的名字命名为Ba;
            SiloHost siloHost = new SiloHost("Ba", config);
            //初始化仓储
            siloHost.InitializeOrleansSilo();
            //启动
            siloHost.StartOrleansSilo();

            //检查一下
            if (siloHost.IsStarted)
            {
                Console.WriteLine("silohost 启动成功");
            }
            else
            {
                Console.WriteLine("启动失败");
            }
            Console.ReadKey();
        }
    }
}
