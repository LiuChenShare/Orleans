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
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo();
            GrainClient.Initialize(config);

            var grainFactory = GrainClient.GrainFactory;
            var e0 = grainFactory.GetGrain<IEmployee>("员工A");
            var e1 = grainFactory.GetGrain<IEmployee>("员工B");
            var e2 = grainFactory.GetGrain<IEmployee>("员工C");
            var e3 = grainFactory.GetGrain<IEmployee>("员工D");
            var e4 = grainFactory.GetGrain<IEmployee>("员工E");

            var m0 = grainFactory.GetGrain<IManager>("经理甲");
            var m1 = grainFactory.GetGrain<IManager>("经理乙");
            var m0e = m0.AsEmployee().Result;
            var m1e = m1.AsEmployee().Result;

            m0e.Promote(10);
            m1e.Promote(11);

            m0.AddDirectReport(e0).Wait();
            m0.AddDirectReport(e1).Wait();
            m0.AddDirectReport(e2).Wait();

            m1.AddDirectReport(m0e).Wait();
            m1.AddDirectReport(e3).Wait();

            m1.AddDirectReport(e4).Wait();

            Console.WriteLine("Orleans Silo 正在运行.\n按回车键结束...");
            Console.ReadLine();
            
        }
    }
}
