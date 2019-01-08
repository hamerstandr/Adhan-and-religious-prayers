using System.Windows;
using اذان_و_اوقات_شرعی.Medol;

namespace اذان_و_اوقات_شرعی
{
    public struct RECT2
    {
        public double Y { get; internal set; }
        public Point Location { get; internal set; }
        public double X { get; internal set; }
        public WindowsTaskbar.Position State { get; internal set; }
    }
}