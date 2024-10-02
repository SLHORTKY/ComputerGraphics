using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial1.Engine.Render;

namespace Tutorial1.Drivers.Gdi.Render
{
    public class RenderHost : Engine.Render.RenderHost
    {
        private Graphics GraphicsHost { get; set; }
        private BufferedGraphics bufferedGraphics { get; set; } 
        private Font curFont { get; set; }

        public RenderHost(IRenderHostSetup setup) : base(setup)
        {
            GraphicsHost = Graphics.FromHwnd(setup.HostHandle);
          
            bufferedGraphics = BufferedGraphicsManager.Current.Allocate(GraphicsHost, new Rectangle(Point.Empty, W.GetClientRectangle(setup.HostHandle).Size));

            curFont = new Font("Consolas", 12);
        }

        public override void Dispose()
        {
            bufferedGraphics.Dispose();
            bufferedGraphics = default;

            GraphicsHost.Dispose();
            GraphicsHost = default;

            base.Dispose();
        }

        protected override void RenderInternal()
        {
            bufferedGraphics.Graphics.Clear(Color.Black);
            bufferedGraphics.Graphics.DrawString($"{FpsCounter.FpsRender}", curFont , Brushes.Red , 0, 0);

            bufferedGraphics.Render();
        }

    }
}
