using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial1.Engine.Render;
using Tutorial1.Utils;

namespace Tutorial1.Drivers.Gdi.Render
{
    public class RenderHost : Engine.Render.RenderHost
    {
        private Graphics GraphicsHost { get; set; }
        private BufferedGraphics BufferedGraphics { get; set; } 
        private IntPtr GraphicsHostDeviceContext { get; set; }
        private DirectBitmap BackBuffer { get; set; }

        private Font curFont { get; set; }

        public RenderHost(IRenderHostSetup setup) : base(setup)
        {
            GraphicsHost = Graphics.FromHwnd(setup.HostHandle);
            GraphicsHostDeviceContext = GraphicsHost.GetHdc();
            CreateBuffer(BufferSize);
            CreateViewPort(ViewPortSize);
            
            curFont = new Font("Consolas", 12);
        }

        public override void Dispose()
        {
            GraphicsHost.ReleaseHdc(GraphicsHostDeviceContext);
            GraphicsHostDeviceContext = default;

            GraphicsHost.Dispose();
            GraphicsHost = default;

            DisposeBuffer();
            DisposeViewPort();
            base.Dispose();
        }

        protected override void ResizeBuffer(Size size)
        {
            base.ResizeBuffer(size);
            DisposeBuffer();
            CreateBuffer(size);
        }

        protected override void ResizeViewPort(Size size)
        {
            base.ResizeViewPort(size);
            DisposeViewPort();
            CreateViewPort(size);
        }

        private void CreateBuffer(Size size)
        {
            BackBuffer = new DirectBitmap(size);

        }
        private void DisposeBuffer()
        {
            BackBuffer.Dispose();
            BackBuffer = default;
        }

        private void CreateViewPort(Size size)
        {
            BufferedGraphics = BufferedGraphicsManager.Current.Allocate(GraphicsHostDeviceContext, new Rectangle(Point.Empty,size));
        }
        private void DisposeViewPort()
        {
            BufferedGraphics.Dispose();
            BufferedGraphics = default;
        }

        protected override void RenderInternal()
        {
            var graphics = BackBuffer.Graphics;
            graphics.Clear(Color.Black);

            var t = DateTime.UtcNow.Millisecond / 1000.0;
            Color GetColor(int x, int y) => Color.FromArgb
            (
                byte.MaxValue,
                (byte)((double)x / BufferSize.Width * byte.MaxValue),
                (byte)((double)y / BufferSize.Height * byte.MaxValue),
                (byte)(Math.Sin(t * Math.PI) * byte.MaxValue)
            );

            Parallel.For(0, BackBuffer.Buffer.Length, index =>
            {
                BackBuffer.GetXY(index, out var x , out var y);
                //BackBuffer.SetPixel(x, y, GetColor(x, y));
                BackBuffer.Buffer[index] = GetColor(x,y).ToArgb();
            });

           
            graphics.DrawString($"{FpsCounter.FpsRender}", curFont , Brushes.Red , 0, 0);

            BufferedGraphics.Graphics.DrawImage(BackBuffer.Bitmap, new RectangleF(PointF.Empty, ViewPortSize),new RectangleF(PointF.Empty, BufferSize),GraphicsUnit.Pixel);

            BufferedGraphics.Render(GraphicsHostDeviceContext);
        }

    }
}
