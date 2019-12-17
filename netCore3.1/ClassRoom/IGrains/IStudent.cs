using System.Threading.Tasks;

namespace IGrains
{
    /// <summary>
    /// 学生
    /// </summary>
    public interface IStudent : Orleans.IGrainWithIntegerKey
    {
        /// <summary>
        /// 打招呼
        /// </summary>
        /// <returns></returns>
        Task<string> SayHello();
    }
}
