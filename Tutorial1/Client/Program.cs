using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial1.Engine.Render;
using Tutorial1.Utils;

namespace Tutorial1.Client
{
    internal class Program :
        System.Windows.Application, IDisposable
    {

        private IReadOnlyList<IRenderHost> renderHosts { get; set; }
        public Program()
        {
            Startup += (sender, args) => Ctor();
            Exit += (sender, args) => Dispose();
        }

        private void Ctor()
        {
            renderHosts = WindowFactory.SeedWindows();

            while (!Dispatcher.HasShutdownStarted)
            {
                Render(renderHosts);
                System.Windows.Forms.Application.DoEvents();
            }
        }
        public void Dispose()
        {
            renderHosts.Foreach(host => host.Dispose());
            renderHosts = default;
        }


        private static void Render(IEnumerable<IRenderHost> renderHosts)
        {
            renderHosts.Foreach(rh => rh.Render());
        }
    }
}
