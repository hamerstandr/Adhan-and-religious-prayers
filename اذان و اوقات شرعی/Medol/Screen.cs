using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace اذان_و_اوقات_شرعی.Medol
{
    internal static class CursorPosition
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct PointInter
        {
            public int X;
            public int Y;
            public static explicit operator Point(PointInter point) => new Point(point.X, point.Y);
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out PointInter lpPoint);

        // For your convenience
        public static Point GetCursorPosition()
        {
            PointInter lpPoint;
            GetCursorPos(out lpPoint);
            return (Point)lpPoint;
        }
    }
    class Screen
    {
        DispatcherTimer timer =new DispatcherTimer();
        public Screen()
        {
            timer.Interval = new TimeSpan(0, 0, 0,1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        public event EventHandler ChangedPosition;
        private Point Temp;
        private void Timer_Tick(object sender, EventArgs e)
        {
            var p = CursorPosition.GetCursorPosition();
            if (Temp != p)
            {
                Temp = p;
                ChangedPosition?.Invoke(p, null);
            }
            
            
        }
    }
}
