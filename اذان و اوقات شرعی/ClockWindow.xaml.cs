using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using اذان_و_اوقات_شرعی.Medol;
using اذان_و_اوقات_شرعی.Properties;

namespace اذان_و_اوقات_شرعی
{
    /// <summary>
    /// Interaction logic for ClockWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {
        System.Globalization.PersianCalendar PersianCalendar1 = new System.Globalization.PersianCalendar();
        System.Globalization.GregorianCalendar GregorianCalendar1 = new System.Globalization.GregorianCalendar();
        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timerHid = new DispatcherTimer();
        public ClockWindow()
        {
            InitializeComponent();
            this.Loaded += ClockWindow_Loaded;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
            Screen1 = new Screen();
            Screen1.ChangedPosition += ClockWindow_ChangedPosition;
            timerHid.Interval = new TimeSpan(0, 0, 0,0,100);
            timerHid.Tick += TimerHid_Tick; ;
        }

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
                Count = 0;this.Hide();
                timerHid.Stop();
            }
        }

        Screen Screen1;
        private void ClockWindow_ChangedPosition(object sender, EventArgs e)
        {
            var w = System.Windows.SystemParameters.PrimaryScreenWidth;
            var h = System.Windows.SystemParameters.PrimaryScreenHeight;
            Point p =(Point) sender;
            if (p.X >= w - 100)
                if (p.Y <= 400)
                {
                    this.Show();
                    this.Activate();
                    timerHid.Start();
                }
        }

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
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                
            }));
        }
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
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
