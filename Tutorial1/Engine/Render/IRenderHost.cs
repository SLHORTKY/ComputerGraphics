using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial1.Engine.Render
{
    public interface IRenderHost : IDisposable
    {
        IntPtr HostHandle { get; }

        FpsCounter FpsCounter { get; }

        void Render();
    }
}
