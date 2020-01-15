using IGrains;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grains
{
    /// <summary>
    /// 学校
    /// </summary>
    public class Classroom : Orleans.Grain, IClassroom
    {
        private List<IStudent> Students = new List<IStudent>();

        /// <summary>
        /// 报名登记并拿到学号
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        public async Task<int> Enroll(string name)
        {
            int studentID = Students.Count() + 1;
            var aaa = this.GetPrimaryKeyLong();
            IStudent student = GrainFactory.GetGrain<IStudent>(studentID);
            await student.SetStudentInfo(studentID, name);//等待一下
            Students.Add(student);
            return studentID;
        }

        /// <summary>
        /// 学生入座
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public Task<bool> Seated(IStudent student)
        {
            if (!Students.Contains(student))
            {
                return Task.FromResult(false);//没登记的学生不给坐
            }
            foreach (var item in Students)
            {
                if (item != student)
                {
                    item.ReceiveMessages("加入新同学", this.GetPrimaryKeyLong(), $"学号{student.GetPrimaryKeyLong()}的童靴加入了我们，大家欢迎");//不等待
                }
            }
            return Task.FromResult(true);
        }

        /// <summary>
        /// 发言
        /// </summary>
        /// <param name="student">当前的学生</param>
        /// <param name="message">发言内容</param>
        public Task<bool> Speech(IStudent student, string message)
        {
            if (!Students.Contains(student))
            {
                return Task.FromResult(false);//没登记的学生闭嘴
            }
            foreach (var item in Students)
            {
                if (item != student)
                {
                    item.ReceiveMessages("同学发言", (int)student.GetPrimaryKeyLong(), message);//不等待
                }
            }
            return Task.FromResult(true);
        }
    }
}
