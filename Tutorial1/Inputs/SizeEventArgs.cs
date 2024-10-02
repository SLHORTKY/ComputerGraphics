using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial1.Inputs
{
    public class SizeEventArgs : ISizeEventArgs
    {
        public System.Drawing.Size NewSize { get; set; }


        public SizeEventArgs(System.Drawing.Size newSize)
        {
            NewSize = newSize;
        }
    }
}
