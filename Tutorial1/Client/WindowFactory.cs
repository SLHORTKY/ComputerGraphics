using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorial1.Engine.Render;
using Tutorial1.Utils;

namespace Tutorial1.Client
{
    public static class WindowFactory
    {
        public static IReadOnlyList<IRenderHost> SeedWindows()
        {
            var size = new System.Drawing.Size(720, 480);

            var renderHosts = new[] 
            {
            CreateWindowForm(size, "Created Form", handle => new Drivers.Gdi.Render.RenderHost(handle)),
            CreateWindowWpf(size, "Created wpf", handle => new  Drivers.Gdi.Render.RenderHost(handle)),
            };
            SortWindows(renderHosts );
            return renderHosts;
        }

        private static System.Windows.Forms.Control CreateHostControl()
        {
            var hostControl = new System.Windows.Forms.Panel {
                Dock = System.Windows.Forms.DockStyle.Fill,
                BackColor = System.Drawing.Color.Transparent,
                ForeColor = System.Drawing.Color.Transparent,
            };
            void EnterFocus(System.Windows.Forms.Control control)
            {
                if (!hostControl.Focused) hostControl.Focus();
            }

            hostControl.MouseEnter += (sender, args) => EnterFocus(hostControl);

            hostControl.MouseClick += (sender, args) => EnterFocus(hostControl);
            return hostControl;
        }

        private static IRenderHost CreateWindowForm(System.Drawing.Size size, string title , Func<IntPtr, IRenderHost> ctorRenderHost)
        {
            var window = new System.Windows.Forms.Form
            {
                Size = size,
                Text = title,
            };
            var hostControl = new System.Windows.Forms.Panel
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                BackColor = System.Drawing.Color.Transparent,
                ForeColor = System.Drawing.Color.Transparent,

            };
            window.Controls.Add(hostControl);
            hostControl.MouseEnter += (sender, args) =>
            {
                if (System.Windows.Forms.Form.ActiveForm != window) window.Activate();
                if (!hostControl.Focused) hostControl.Focus();
            };

            window.FormClosed += (sender, args) =>
            {
                System.Windows.Application.Current.Shutdown();
            };

            window.Show();

            return ctorRenderHost(hostControl.Handle());
        }
        private static IRenderHost CreateWindowWpf(System.Drawing.Size size, string title, Func<IntPtr, IRenderHost> ctorRenderHost)
        {
            var window = new System.Windows.Window
            {
                Width = size.Width,
                Height = size.Height,
                Title = title,
            };

            var hostControl = CreateHostControl();
            var windowsFormHost = new System.Windows.Forms.Integration.WindowsFormsHost();
            windowsFormHost.Child = hostControl;

            window.Content = windowsFormHost;

            window.Closed += (sender, args) => { System.Windows.Application.Current.Shutdown(); };
            window.Show();

            return ctorRenderHost(hostControl.Handle());
        }
   
        private static void SortWindows(IEnumerable<IRenderHost> renderHosts)
        {
            var windowsinfo = renderHosts.Select(renderHost => new WindowInfo(renderHost.HostHandle).GetRoot()).ToArray();
            var maxsize = new System.Drawing.Size(windowsinfo.Max(a => a.RectangleWindow.Width), windowsinfo.Max(a => a.RectangleWindow.Height));
            
            var maxcolumns = (int)Math.Ceiling(Math.Sqrt(windowsinfo.Length));
            var maxrows = (int)Math.Ceiling((double) windowsinfo.Length / maxcolumns);

            var primaryScreen =  System.Windows.Forms.Screen.PrimaryScreen;
            var left =  primaryScreen.WorkingArea.Width / 2 - maxcolumns * maxsize.Width / 2;
            var top = primaryScreen.WorkingArea.Height / 2 - maxrows * maxsize.Height / 2;

            for(var row = 0; row < maxrows; row++)
            {
                for (var col = 0; col < maxcolumns; col++)
                {
                    var i = row * maxcolumns + col;
                    if (i >= windowsinfo.Length) return;

                    var x = col *maxsize.Width + left;
                    var y = row *maxsize.Height + top;

                    var windowinfo = windowsinfo[i];

                    User32.MoveWindow(windowinfo.Handle, x, y, windowinfo.RectangleWindow.Width, windowinfo.RectangleWindow.Height, false);
                }
            }
        }
    }
}
