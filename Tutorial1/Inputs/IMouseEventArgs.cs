﻿using MathNet.Spatial.Euclidean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tutorial1.Inputs
{
    public interface IMouseEventArgs
    {
        Point2D Position { get;}
        MouseButtons Buttons { get; }

        int WheelDelta { get; }
        int ClickCount { get; }
    }
}
