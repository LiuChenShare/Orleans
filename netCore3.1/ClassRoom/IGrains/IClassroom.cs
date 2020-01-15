using System.Threading.Tasks;

namespace IGrains
{
    /// <summary>
    /// 教室
    /// </summary>
    public interface IClassroom : Orleans.IGrainWithIntegerKey
    {
        /// <summary>
        /// 报名登记并拿到学号
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        Task<int> Enroll(string name);

        /// <summary>
        /// 学生入座
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Task<bool> Seated(IStudent student);

        /// <summary>
        /// 发言
        /// </summary>
        /// <param name="student">当前的学生</param>
        /// <param name="message">发言内容</param>
        /// <returns></returns>
        Task<bool> Speech(IStudent student, string message);
    }
}
