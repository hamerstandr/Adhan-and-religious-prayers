using AppBarApplication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using اذان_و_اوقات_شرعی.Interop;
using اذان_و_اوقات_شرعی.Medol;
using اذان_و_اوقات_شرعی.Properties;
using اذان_و_اوقات_شرعی.Service;

namespace اذان_و_اوقات_شرعی
{
    /// <summary>
    /// Interaction logic for ClockWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {
        readonly PersianCalendar PersianCalendar1 = new PersianCalendar();
        readonly GregorianCalendar GregorianCalendar1 = new GregorianCalendar();
        readonly DispatcherTimer timer = new DispatcherTimer();
        readonly DispatcherTimer timerHid = new DispatcherTimer();
        public ClockWindow()
        {
            InitializeComponent();
            this.Loaded += ClockWindow_Loaded;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Screen1 = new PositionMouseInScreen();
            Screen1.ChangedPosition += ClockWindow_ChangedPosition;
            timerHid.Interval = new TimeSpan(0, 0, 0,0,100);
            timerHid.Tick += TimerHid_Tick;
            //Win.Show();
        }
        public void Enable()
        {
            Screen1.ChangedPosition += ClockWindow_ChangedPosition;
        }
        public void Disable()
        {
            Screen1.ChangedPosition -= ClockWindow_ChangedPosition;
        }
        PositionMouseInScreen screen;
        internal PositionMouseInScreen Screen1 { get => screen; set => screen = value; }
        private void ClockWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.X != 0)
            {
                this.Left = Settings.Default.X;
                this.Top = Settings.Default.Y;
            }
        }

        int Count = 0;
        private void TimerHid_Tick(object sender, EventArgs e)
        {
            Count++;
            if (Count == 50)
            {
                Count = 0;
                this.Hide();
                timerHid.Stop();
            }
        }
        internal static bool IsRTL => CultureInfo.InstalledUICulture.TextInfo.IsRightToLeft;



        RECT2 GetSetTasck(double Height, double Width)
        {
            RECT2 p =new RECT2();
            //گرفتن موقعیت ساعت سیستم
            var taskbarState = WindowsTaskbar.Current;
            if (taskbarState.ContainingScreen == null)
            {
                // We're not ready to lay out. (e.g. RDP transition)
                var w = System.Windows.SystemParameters.PrimaryScreenWidth;
                var h = System.Windows.SystemParameters.PrimaryScreenHeight;
                p.X = w- Width; p.Y = h- Height;
                return p;
            }
            p.State = taskbarState.Location;
            switch (taskbarState.Location)
            {
                case WindowsTaskbar.Position.Left:
                    p.X = taskbarState.Size.Right +Width;
                    p.Y = taskbarState.Size.Bottom - Height;

                    p.Location = new Point(taskbarState.Size.Left, taskbarState.Size.Bottom);
                    break;
                case WindowsTaskbar.Position.Right:
                    p.X = taskbarState.Size.Left  - Width;
                    p.Y = taskbarState.Size.Bottom - Height;

                    p.Location = new Point(taskbarState.Size.Right, taskbarState.Size.Bottom);
                    break;
                case WindowsTaskbar.Position.Top:
                    p.X = IsRTL ? taskbarState.Size.Left  :
                        taskbarState.Size.Right - Width;
                    p.Y = taskbarState.Size.Bottom + Height;

                    p.Location = new Point(IsRTL ? taskbarState.Size.Left :
                        taskbarState.Size.Right, taskbarState.Size.Top);
                    break;
                case WindowsTaskbar.Position.Bottom:
                    p.X = IsRTL ? taskbarState.Size.Left  :
                        taskbarState.Size.Right  - Width;
                    p.Y = taskbarState.Size.Top - Height;

                    p.Location = new Point(IsRTL ? taskbarState.Size.Left :
                        taskbarState.Size.Right, taskbarState.Size.Bottom);
                    break;
            }

            return p;
        }

        //readonly Window1 Win = new Window1();
        private void ClockWindow_ChangedPosition(object sender, EventArgs e)
        {
            if (Settings.Default.AutoHideClock)
            {
                var PSet = GetSetTasck(0, 150);
                //Win.Left = PSet.X;
                //Win.Top = PSet.Y;
                Point p = (Point)sender;
                switch (PSet.State)
                {
                    case WindowsTaskbar.Position.Top:
                        if (p.X >= PSet.X && p.X <= PSet.Location.X)
                            if (p.Y <= PSet.Y && p.Y >= PSet.Location.Y)
                                ShowApp();
                        break;
                    case WindowsTaskbar.Position.Bottom:
                        if (p.X >= PSet.X && p.X <= PSet.Location.X)
                            if (p.Y >= PSet.Y && p.Y <= PSet.Location.Y)
                                ShowApp();
                        break;
                    case WindowsTaskbar.Position.Right:
                        if (p.X >= PSet.X && p.X <= PSet.Location.X)
                            if (p.Y >= PSet.Y && p.Y <= PSet.Location.Y)
                                ShowApp();
                        break;
                    case WindowsTaskbar.Position.Left:
                        if (p.X <= PSet.X && p.X >= PSet.Location.X)
                            if (p.Y >= PSet.Y && p.Y <= PSet.Location.Y)
                                ShowApp();
                        break;
                }
                //
                //var w = System.Windows.SystemParameters.PrimaryScreenWidth;
                //var h = System.Windows.SystemParameters.PrimaryScreenHeight;
            }
        }
        void ShowApp()
        {
            this.Show();
            this.Activate();
            timerHid.Start();
            //Win.Show();
            //Win.Activate();

        }
        //set time
        private void Timer_Tick(object sender, EventArgs e)
        {
            
            if (Refresh == 120)
            {
                SetDate();
                Refresh = 0;
            }
            Refresh++;
            secondHand.Angle = DateTime.Now.Second * 6;
            minuteHand.Angle = DateTime.Now.Minute * 6;
            hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5);
        }

        int Refresh = 120;
        //void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
        //    {
                
        //    }));
        //}
        void SetDate()
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, now.Day, new System.Globalization.GregorianCalendar());
            DateTime dateP = new DateTime(now.Year, now.Month, now.Day, new System.Globalization.PersianCalendar());
            string currentTime = date.ToShortTimeString();

            string DoW = DateTime.Now.PersionDayOfWeek();
            persianCalendar.Content = $"{PersianCalendar1.GetYear(DateTime.Now)}/{PersianCalendar1.GetMonth(DateTime.Now)}/{PersianCalendar1.GetDayOfMonth(DateTime.Now)}  {DateTime.Now.PersionDayOfWeek()}";
            christianityCalendar.Content = $"{GregorianCalendar1.GetYear(DateTime.Now)}/{GregorianCalendar1.GetMonth(DateTime.Now)}/{GregorianCalendar1.GetDayOfMonth(DateTime.Now)}  {GregorianCalendar1.GetDayOfWeek(DateTime.Now)}";
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            if (e.ClickCount == 2)
            {
                MainWindow.ShowPopup();
            }
        }
        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Settings.Default.X = this.Left;
            Settings.Default.Y = this.Top;
            Settings.Default.Save();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Settings.Default.X = this.Left;
            Settings.Default.Y = this.Top;
            Settings.Default.Save();
        }
    }
}
