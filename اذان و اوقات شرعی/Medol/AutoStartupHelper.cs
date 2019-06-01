using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using اذان_و_اوقات_شرعی.Interop;

namespace اذان_و_اوقات_شرعی.Medol
{
    internal static class AutoStartupHelper
    {
        private static readonly string StartupFullPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Startup),
            Path.ChangeExtension(Path.GetFileName(App.AppFullPath), ".lnk"));

        internal static void CreateAutorunShortcut()
        {
            try
            {
                RemoveAutorunShortcut();

                var lnk = (IShellLinkW)new ShellLink();

                lnk.SetPath(App.AppFullPath);
                lnk.SetArguments("/autorun"); // silent
                lnk.SetIconLocation(App.AppFullPath, 0);
                lnk.SetWorkingDirectory(App.AppPath);
                ((IPersistFile)lnk).Save(StartupFullPath, false);
            }
            catch (Exception e)
            {
            }
        }

        internal static void RemoveAutorunShortcut()
        {
            try
            {
                if (IsAutorun())
                File.Delete(StartupFullPath);
            }
            catch (Exception e)
            {
                
            }
        }

        internal static bool IsAutorun()
        {
            return File.Exists(StartupFullPath);
        }
    }
}
