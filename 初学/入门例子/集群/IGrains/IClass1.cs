﻿using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGrains
{
    public interface IClass1 : IGrainWithIntegerKey
    {
        Task<string> SayHello();
    }
}
