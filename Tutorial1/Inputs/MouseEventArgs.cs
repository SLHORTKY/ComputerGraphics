﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Spatial.Euclidean;
using System.Threading.Tasks;
using Tutorial1.Mathematics.Extensions;

namespace Tutorial1.Inputs
{
    internal class MouseEventArgs : EventArgs, IMouseEventArgs
    {
        public Point2D Position { get; }

        public MouseButtons Buttons { get; }

        public int WheelDelta { get; }

        public int ClickCount { get; }

        public MouseEventArgs(Point2D position, bool buttonLeft, bool buttonMiddle, bool buttonRight, bool buttonX1, bool buttonX2, int wheelDelta, int clickCount)
        {
            Position = position;
            Buttons |= buttonLeft ? MouseButtons.Left : MouseButtons.None;
            Buttons |= buttonMiddle ? MouseButtons.Middle : MouseButtons.None;
            Buttons |= buttonRight ? MouseButtons.Right : MouseButtons.None;
            Buttons |= buttonX1 ? MouseButtons.XButton1 : MouseButtons.None;
            Buttons |= buttonX2 ? MouseButtons.XButton2 : MouseButtons.None;
            WheelDelta = wheelDelta;
            ClickCount = clickCount;
        }

        /// <inheritdoc />
        public MouseEventArgs(System.Windows.Forms.MouseEventArgs args) :
            this
            (
                args.Location.ToPoint2D(),
                (args.Button & System.Windows.Forms.MouseButtons.Left) != 0,
                (args.Button & System.Windows.Forms.MouseButtons.Middle) != 0,
                (args.Button & System.Windows.Forms.MouseButtons.Right) != 0,
                (args.Button & System.Windows.Forms.MouseButtons.XButton1) != 0,
                (args.Button & System.Windows.Forms.MouseButtons.XButton2) != 0,
                args.Delta,
                args.Clicks
            )
        {
        }

        /// <inheritdoc />
        public MouseEventArgs(System.Windows.Input.MouseEventArgs args, Point2D position, int wheelDelta) :
            this
            (
                position,
                args.LeftButton == System.Windows.Input.MouseButtonState.Pressed,
                args.MiddleButton == System.Windows.Input.MouseButtonState.Pressed,
                args.RightButton == System.Windows.Input.MouseButtonState.Pressed,
                args.XButton1 == System.Windows.Input.MouseButtonState.Pressed,
                args.XButton2 == System.Windows.Input.MouseButtonState.Pressed,
                wheelDelta,
                0
            )
        {
        }

        /// <inheritdoc />
        public MouseEventArgs(System.Windows.Input.MouseWheelEventArgs args, Point2D position) :
            this(args, position, args.Delta)
        {
        }
    }
}
