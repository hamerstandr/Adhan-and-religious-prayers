using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using اذان_و_اوقات_شرعی.Properties;
using اذان_و_اوقات_شرعی.Service;

namespace اذان_و_اوقات_شرعی
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            if (Settings.Default.AutoUpdate)
            {
                System.Timers.Timer myTimer = new System.Timers.Timer
                {
                    AutoReset = false,
                    Interval = 20000
                };
                myTimer.Elapsed += MyTimer_Elapsed_AutoCheckUpdate;
                myTimer.Enabled = true;
            }
        }
        private void MyTimer_Elapsed_AutoCheckUpdate(object sender, ElapsedEventArgs e)
        {
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply pr = ping.Send("www.google.com", 12000);
                if (pr.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    checkUpdateManager.CheckForUpdates(false);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.ToString());
            }

        }
        private UDMap udMap = new UDMap();
        private PortProcessMap portProcessMap = PortProcessMap.GetInstance();
        private static CheckUpdateManager checkUpdateManager = new CheckUpdateManager();
        public static void TryToSetAutoUpdate(bool autoUpdate)
        {
            Settings.Default.AutoUpdate = autoUpdate;
            Settings.Default.Save();
        }

        public static void TryToCheckUpdate()
        {
            checkUpdateManager.CheckForUpdates(true);
        }
    }
}
