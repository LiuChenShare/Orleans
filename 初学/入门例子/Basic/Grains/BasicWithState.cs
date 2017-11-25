using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    [Orleans.Providers.StorageProvider(ProviderName = "OrleansTest")]
    public class BasicWithState : Orleans.Grain<IGrains.BasicState>, IGrains.IBasicWithState
    {
        public override Task OnActivateAsync()
        {
            //从数据库中读取保存的State
            this.ReadStateAsync();
            return base.OnActivateAsync();
        }

        public override Task OnDeactivateAsync()
        {
            //把State保存到数据库
            this.WriteStateAsync();
            return base.OnDeactivateAsync();
        }

        public Task<List<string>> GetHistory()
        {
            return Task.FromResult(this.State.historyStr);
        }

        public Task SayHello(string str)
        {
            this.State.historyStr.Add(str);
            //把State保存到数据库
            this.WriteStateAsync();
            return Task.CompletedTask;
        }
    }
}

