using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial1.Inputs;

namespace Tutorial1.Engine.Render
{
    public class RenderHostSetup : IRenderHostSetup
    {
        public IntPtr HostHandle { get; }

        public IInput HostInput { get; }


        public RenderHostSetup(IntPtr hostHandle, IInput hostInput)
        {
            HostHandle = hostHandle;
            HostInput = hostInput;
        }
    }
}
