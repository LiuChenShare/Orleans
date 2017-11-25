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
        public string From { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
    }
}
