using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    [Immutable]
    public class GreetingData
    {
        /// <summary>
        /// 发送消息的人
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 已发送的消息数量
        /// </summary>
        public int Count { get; set; }
    }
}
