using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host2
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Orleans.Runtime.Configuration.ClusterConfiguration();
            config.LoadFromFile("Host.xml");
            //SiloHost siloHost = new SiloHost(System.Net.Dns.GetHostName());
            //初始化一个silohost,这里使用了Orleans提供的silohost而不是silo,其中silo的名字命名为Ba;
            Orleans.Runtime.Host.SiloHost siloHost = new Orleans.Runtime.Host.SiloHost("Ba", config);
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
