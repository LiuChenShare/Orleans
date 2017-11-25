using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IEmployee : IGrainWithGuidKey
    {
        /// <summary>
        /// 提升员工的等级
        /// </summary>
        Task<int> GetLevel();

    }
}
