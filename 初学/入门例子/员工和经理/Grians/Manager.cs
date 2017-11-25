using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGrains;
using Orleans;

namespace Grians
{
    public class Manager : Orleans.Grain, IGrains.IManager
    {
        private IEmployee _me;
        private List<IEmployee> _reports = new List<IEmployee>();
        
        public override Task OnActivateAsync()
        {
            _me = this.GrainFactory.GetGrain<IEmployee>(this.GetPrimaryKeyString());
            return base.OnActivateAsync();
        }

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task AddDirectReport(IEmployee employee)
        {
            _reports.Add(employee);
            await employee.SetManager(this);
            //await employee.Greeting(_me, string.Format("欢迎{0}来到我的团队", employee.GetPrimaryKeyString()));
            await employee.Greeting(new GreetingData
            {
                From = this.GetPrimaryKeyString(),
                Message = "欢迎来到我的团队!"
            });
        }

        public Task<IEmployee> AsEmployee()
        {
            return Task.FromResult(_me);
        }

        public Task<List<IEmployee>> GetDirectReports()
        {
            return Task.FromResult(_reports);
        }

    }
}
