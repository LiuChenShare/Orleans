using IGrains;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            //等待服务端启动完毕
            Console.WriteLine("按回车键开始连接服务器...");
            Console.ReadKey();

            // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo();
            GrainClient.Initialize(config);

            var grainFactory = GrainClient.GrainFactory;
            var e0 = GrainClient.GrainFactory.GetGrain<IEmployee>("员工A");
            var m1 = GrainClient.GrainFactory.GetGrain<IManager>("经理甲");
            m1.AddDirectReport(e0).Wait();

            Console.WriteLine("Orleans Silo 正在运行.\n按回车键结束...");
            Console.ReadLine();

        }
    }
}
