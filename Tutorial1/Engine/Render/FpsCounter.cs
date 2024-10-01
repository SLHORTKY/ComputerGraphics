using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial1.Engine.Render
{
    public class FpsCounter : IDisposable
    {
        public TimeSpan UpdateRate { get; }
        private Stopwatch stopWatchUpdate { get; set; }
        private Stopwatch stopWatchFrame { get; set; }
        private TimeSpan Elapsed { get; set; }
        private int FrameCount { get; set; }
        public double FpsRender { get; private set; }
        public double FpsGlobal { get; private set; } 

        public FpsCounter(TimeSpan updateRate) 
        {
            UpdateRate = updateRate;
            stopWatchUpdate = new Stopwatch();
            stopWatchFrame = new Stopwatch();

            stopWatchFrame.Start();
            stopWatchUpdate.Start();

            Elapsed = TimeSpan.Zero;
        }

        public void Dispose()
        {
            stopWatchUpdate.Stop();
            stopWatchUpdate = default;

            stopWatchFrame.Stop();
            stopWatchFrame = default;
        }   

        public void StartFrame()
        {
            stopWatchFrame.Restart();
        }

        public void StopFrame()
        {
            stopWatchFrame.Stop();

            Elapsed += stopWatchFrame.Elapsed;
            FrameCount++;


            var updateElapsed = stopWatchUpdate.Elapsed;
            if(updateElapsed > UpdateRate ) 
            {
                FpsRender = FrameCount / Elapsed.TotalSeconds;
                FpsGlobal = FrameCount / updateElapsed.TotalSeconds;

                stopWatchUpdate.Restart();
                Elapsed = TimeSpan.Zero;
                FrameCount = 0;
            }
        }
    }
}
