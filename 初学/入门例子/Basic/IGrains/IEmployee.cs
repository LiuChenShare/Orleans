using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IEmployee : IGrainWithStringKey
    {
        /// <summary>
        /// 获得员工的等级
        /// </summary>
        Task<int> GetLevel();
        /// <summary>
        /// 提升员工的等级
        /// </summary>
        /// <param name="newLevel"></param>
        /// <returns></returns>
        Task Promote(int newLevel);
        /// <summary>
        /// 获得管理员工的经理
        /// </summary>
        /// <returns></returns>
        Task<IManager> GetManager();
        /// <summary>
        /// 设置此员工归属的经理
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        Task SetManager(IManager manager);
        /// <summary>
        /// 表示某个员工向自己发送了一个问候
        /// </summary>
        /// <param name="from"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Greeting(GreetingData data);
    }

    public interface IManager : IGrainWithStringKey
    {
        /// <summary>
        /// 经理也可以是员工
        /// </summary>
        /// <returns></returns>
        Task<IEmployee> AsEmployee();
        /// <summary>
        /// 获得经理的直属员工
        /// </summary>
        /// <returns></returns>
        Task<List<IEmployee>> GetDirectReports();
        /// <summary>
        /// 把员工加入到自己的直属员工
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task AddDirectReport(IEmployee employee);
    }
}
