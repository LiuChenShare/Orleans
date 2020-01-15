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

        /// <summary>
        /// 设置个人信息
        /// </summary>
        /// <param name="studentId">学号</param>
        /// <param name="studentName">姓名</param>
        /// <returns></returns>
        Task SetStudentInfo(int studentId, string studentName);

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="code">消息code类型</param>
        /// <param name="senderId">消息发送人id</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        Task ReceiveMessages(string code, object senderId, string message);
    }
}
