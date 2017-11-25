using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class BasicGrain : Grain, IGrains.IBasic
    {
        public Task<string> DelayeMsg(string hellostr)
        {
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("{0}：延迟的消息---{1}", DateTime.Now.ToString("HH:mm:ss.fff"), hellostr);
            return Task.FromResult<string>("延迟的done");
        }

        public Task<string> SayHello(string hellostr)
        {
            Console.WriteLine("{0} : {1}", DateTime.Now.ToString("HH：mm:ss.fff"), hellostr);
            return Task.FromResult<string>("done");
        }
    }
}
