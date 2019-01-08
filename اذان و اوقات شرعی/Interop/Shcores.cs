using System;
using System.Runtime.InteropServices;

namespace اذان_و_اوقات_شرعی.Interop
{
    class Shcores
    {
        public enum DpiType : int
        {
            Effective = 0,
            Angular = 1,
            Raw = 2,
        }

        [DllImport("Shcore.dll", PreserveSig = true)]
        internal static extern HRESULT GetDpiForMonitor(
            IntPtr hMonitor,
            DpiType dpiType,
            out uint dpiX,
            out uint dpiY);
    }
}
