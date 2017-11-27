using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class Class1 : Grain, IGrains.IClass1
    {
        public Task<string> SayHello()
        {
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("{0}：大家好，我是{0}", this.GetPrimaryKeyLong());
            return Task.FromResult<string>(string.Format("玩家{0}已登录", this.GetPrimaryKeyLong()));
        }
    }
}
