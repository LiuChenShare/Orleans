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
            Console.ReadKey();
            //然后我聪明的敲击了回车键
            Run();
            Console.ReadKey();
        }

        static async void Run()
        {
            //利用内置方法获得一个配置类,这个类指明服务端的端口是1234
            //可以利用配置文件，不过这里我就先用这个简单的配置类
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(1234);

            //初始化一个GrainClient
            GrainClient.Initialize(config);

            //从silo处，获得了一个BasicGrain的接口
            IGrains.IBasic agrain = GrainClient.GrainFactory.GetGrain<IGrains.IBasic>(314);
            IGrains.IBasic agrain2 = GrainClient.GrainFactory.GetGrain<IGrains.IBasic>(315);


            agrain.DelayeMsg("你好吗？");
            agrain2.DelayeMsg("让我先说");

            //调用里面的方法，等待它返回
            string result = await agrain.SayHello("还好");
            Console.WriteLine(result);


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

        }
    }
}
