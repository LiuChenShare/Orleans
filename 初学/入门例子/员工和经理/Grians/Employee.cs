using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGrains;
using Orleans;
using Orleans.Concurrency;

namespace Grians
{
    [Reentrant]//标记Grain等待任务完成时可以进行额外的调用，实现交错执行
    public class Employee : Orleans.Grain, IGrains.IEmployee
    {
        private int _level;
        private IManager _manager;

        public Task<int> GetLevel()
        {
            return Task.FromResult(_level);
        }

        public Task<IManager> GetManager()
        {
            return Task.FromResult(_manager);
        }

        /// <summary>
        /// 升级（设定等级）
        /// </summary>
        /// <param name="newLevel"></param>
        /// <returns></returns>
        public Task Promote(int newLevel)
        {
            _level = newLevel;
            return Task.CompletedTask;
        }

        public Task SetManager(IManager manager)
        {
            _manager = manager;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="from"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Greeting(GreetingData data)
        {
            Console.WriteLine("{0} said: {1}", data.From, data.Message);
            //停止重复发送
            if (data.Count >=3 )
            {
                return;
            }
            //发送消息
            var formGrain = GrainFactory.GetGrain<IEmployee>(data.From);
            await formGrain.Greeting(new GreetingData
            {
                From = this.GetPrimaryKeyString(),
                Message = "谢谢！",
                Count = data.Count + 1
            });
        }
    }
}
