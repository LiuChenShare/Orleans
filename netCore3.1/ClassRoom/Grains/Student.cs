using IGrains;
using System;
using System.Threading.Tasks;

namespace Grains
{
    /// <summary>
    /// 学生 
    /// </summary>
    public class Student : Orleans.Grain, IStudent
    {
        /// <summary>
        /// 打招呼
        /// </summary>
        /// <returns></returns>
        public Task<string> SayHello()
        {
            var id = this.GrainReference.GrainIdentity.PrimaryKeyLong;//当前Grain的key
            Console.WriteLine($"\n {id}收到SayHello消息 \n");
            return Task.FromResult($"\n 大家好，我是{id} \n");
        }
    }
}
