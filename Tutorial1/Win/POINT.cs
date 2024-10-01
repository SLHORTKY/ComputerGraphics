using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial1.Win
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x, y;

        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public POINT(System.Drawing.Point point) :
            this(point.X, point.Y)   
        {
        }

        public static implicit operator System.Drawing.Point(POINT point) => new System.Drawing.Point(point.x, point.y);
        public static implicit operator POINT(System.Drawing.Point point) => new POINT(point.X,point.Y);
    }
}
