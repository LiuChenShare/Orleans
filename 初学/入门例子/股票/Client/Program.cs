using IGrains;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //等待服务端启动完毕
            Console.WriteLine("按回车键开始连接服务器...");
            Console.ReadKey();

            // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(1234);
            GrainClient.Initialize(config);

            // 检索百度的股票
            var grain = GrainClient.GrainFactory.GetGrain<IStockGrain>("北京时间");
            var price = grain.GetPrice().Result;
            Console.WriteLine(price);

            Console.WriteLine("Orleans Silo 正在运行.\n按回车键结束...");
            Console.ReadLine();
        }
    }
}
