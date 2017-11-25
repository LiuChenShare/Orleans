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
            Console.ReadKey();
            //然后我聪明的敲击了回车键
            Run();
            Console.ReadKey();
        }

        static async void Run()
        {
            //初始化一个GrainClient
            GrainClient.Initialize("Client.xml");

            Console.WriteLine("持久化开始");
            //从silo处，获得了一个BasicGrain的接口
            var agrain = GrainClient.GrainFactory.GetGrain<IGrains.IBasicWithState>("453");
            agrain.SayHello("说了1个Hello");
            var his = await agrain.GetHistory();
            foreach(var item in his)
            {
                Console.WriteLine(item);
            }
            agrain.SayHello("说了2个Hello");
            
        }
    }
}
