﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial1.Inputs;

namespace Tutorial1.Engine.Render
{
    public interface IRenderHostSetup
    {
        IntPtr HostHandle { get; }
        IInput HostInput { get; }
    }
}