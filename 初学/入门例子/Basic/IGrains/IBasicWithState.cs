using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IBasicWithState : IGrainWithStringKey
    {
        /// <summary>
        /// 新增消息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        Task SayHello(string str);
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <returns></returns>
        Task<List<string>> GetHistory();
    }

    /// <summary>
    /// 目的是为了保存状态
    /// </summary>
    public class BasicState
    {
        public List<string> historyStr = new List<string>();
    }
}
