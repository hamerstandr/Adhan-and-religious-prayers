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

namespace اذان_و_اوقات_شرعی
{
    /// <summary>
    /// Interaction logic for ClockWindow.xaml
    /// </summary>
    public partial class ClockWindow : Window
    {
        System.Globalization.PersianCalendar PersianCalendar1 = new System.Globalization.PersianCalendar();
        System.Globalization.GregorianCalendar GregorianCalendar1 = new System.Globalization.GregorianCalendar();
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public ClockWindow()
        {
            InitializeComponent();
            

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;
        }
        int Refresh = 0;
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                Refresh++;
            if (Refresh == 120)
                {
                    SetDate();
                    Refresh = 0;
                }
                secondHand.Angle = DateTime.Now.Second * 6;
                minuteHand.Angle = DateTime.Now.Minute * 6;
                hourHand.Angle = (DateTime.Now.Hour * 30) + (DateTime.Now.Minute * 0.5);
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

            }
        }
    }
}
