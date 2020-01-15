using IGrains;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Grains
{
    /// <summary>
    /// 学生 
    /// </summary>
    public class Student : Orleans.Grain, IStudent
    {
        /// <summary> 学号 </summary>
        private int Id;
        /// <summary> 姓名 </summary>
        private string Name;

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

        public Task SetStudentInfo(int studentId, string studentName)
        {
            Id = studentId;
            Name = studentName;
            return Task.CompletedTask;
        }

        public Task ReceiveMessages(string code, object senderId, string message)
        {
            switch (code)
            {
                case "加入新同学":
                    {
                        ConsoleHelper.WriteSuccessLine($"【{Name}】：欢迎新同学");
                        break;
                    }
                case "同学发言":
                    {
                        ConsoleHelper.WriteSuccessLine($"【{Name}】听到了学号为【{senderId}】的同学说的【{message}】");
                        break;
                    }
                default:
                    {
                        ConsoleHelper.WriteSuccessLine($"【{Name}】：我听不懂你们在说啥");
                        break;
                    }
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 此方法在激活Grain的过程结束时调用
        /// </summary>
        /// <returns></returns>
        public override Task OnActivateAsync()
        {
            Guid primaryKey = this.GetPrimaryKey();
            return base.OnActivateAsync();
        }

    }
}
