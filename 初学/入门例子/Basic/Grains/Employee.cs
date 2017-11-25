using IGrains;
using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    [Reentrant]//防止死锁
    public class Employee : Grain, IEmployee
    {
        private int _level;
        private IManager _manager;

        public Task<int> GetLevel()
        {
            return Task.FromResult(_level);
        }

        public Task Promote(int newLevel)
        {
            _level = newLevel;
            return Task.CompletedTask;
        }

        public Task<IManager> GetManager()
        {
            return Task.FromResult(_manager);
        }

        public Task SetManager(IManager manager)
        {
            _manager = manager;
            return Task.CompletedTask;
        }

        public async Task Greeting(GreetingData data)
        {
            Console.WriteLine("{0} said: {1}", data.From, data.Message);

            //如果超过3次，就停止
            if (data.Count >= 3) return;

            //发送消息给发送方
            var fromGrain = GrainFactory.GetGrain<IEmployee>(data.From);
            await fromGrain.Greeting(new GreetingData()
            {
                From = this.GetPrimaryKeyString(),
                Message = "谢谢！",
                Count = data.Count + 1
            });
        }
    }

    [Reentrant]//防止死锁
    public class Manager : Grain, IManager
    {
        private IEmployee _me;
        private List<IEmployee> _reports = new List<IEmployee>();

        public override Task OnActivateAsync()
        {
            //获取和自己有一样标识的一个IEmployee类来代表自己，注意在Grain调用GerGrain的方法
            _me = this.GrainFactory.GetGrain<IEmployee>(this.GetPrimaryKeyString());
            return base.OnActivateAsync();
        }

        public Task<List<IEmployee>> GetDirectReports()
        {
            return Task.FromResult(_reports);
        }

        public async Task AddDirectReport(IEmployee employee)
        {
            _reports.Add(employee);
            await employee.SetManager(this);
            await employee.Greeting(new GreetingData() {
                From = this.GetPrimaryKeyString(),
                Message = "欢迎来到我的团队!"
            });//发送给新人一个问候
            return;
        }

        public Task<IEmployee> AsEmployee()
        {
            return Task.FromResult(_me);
        }
    }
}
