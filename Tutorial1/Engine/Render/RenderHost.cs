using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial1.Inputs;

namespace Tutorial1.Engine.Render
{
    public abstract class RenderHost : IRenderHost
    {
        public IntPtr HostHandle { get; private set; }

        public IInput HostInput { get; private set; }

        protected Size BufferSize { get; private set; }

        protected Size ViewPortSize { get; private set; }

        public FpsCounter FpsCounter { get; private set; }

        protected RenderHost(IRenderHostSetup setup)
        {
            HostHandle = setup.HostHandle;
            HostInput = setup.HostInput;
            BufferSize = HostInput.Size;
            ViewPortSize = HostInput.Size;

            FpsCounter = new FpsCounter(new TimeSpan(0,0,0,0,1000));

            HostInput.SizeChanged += HostInputOnSizeChanged;
        }

        public virtual void Dispose()
        {
            HostInput.SizeChanged -= HostInputOnSizeChanged;

            FpsCounter.Dispose();
            FpsCounter = default;

            HostInput.Dispose();
            HostInput = default;

            HostHandle = default;
        }

        private void HostInputOnSizeChanged(object sender, ISizeEventArgs args)
        {
            var size = args.NewSize;
            if(size.Width < 1 || size.Height < 1)
            {
                size = new Size(1, 1);
            }

            ResizeBuffer(size);
            ResizeViewPort(size);
        }
        protected virtual void ResizeBuffer(Size size)
        {
            BufferSize = size;
        }
        protected virtual void ResizeViewPort(Size size)
        {
            ViewPortSize = size;
        }

        public void Render()
        {
            FpsCounter.StartFrame();

            RenderInternal();

            FpsCounter.StopFrame();
        }

        protected abstract void RenderInternal();

    }
}
