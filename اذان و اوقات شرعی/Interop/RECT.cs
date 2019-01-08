using System.Runtime.InteropServices;
namespace اذان_و_اوقات_شرعی.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
