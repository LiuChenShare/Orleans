using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IEmployee : Orleans.IGrainWithStringKey
    {
        Task<int> GetLevel();
        /// <summary>
        /// 升级（设定等级）
        /// </summary>
        /// <param name="newLevel"></param>
        /// <returns></returns>
        Task Promote(int newLevel);

        Task<IManager> GetManager();
        Task SetManager(IManager manager);
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="from"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Greeting(GreetingData data);
    }
}
