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
            Console.WriteLine("按Enter键结束");
            Console.ReadKey();
        }

        static async void Run()
        {
            //初始化一个GrainClient
            GrainClient.Initialize("Client.xml");

            Console.WriteLine("持久化开始");
            //从silo处，获得了一个BasicGrain的接口
            for (int i = 0; i < 10; i++)
            {
                var agrain = GrainClient.GrainFactory.GetGrain<IGrains.IClass1>(i+1);
                var say = agrain.SayHello();
                Console.WriteLine(say.Result);
            }
            Console.WriteLine("按Enter键结束");
            Console.ReadKey();
        }
    }
}
