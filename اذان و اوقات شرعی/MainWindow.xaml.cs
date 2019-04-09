using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using اذان_و_اوقات_شرعی.Properties;

namespace اذان_و_اوقات_شرعی
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public DataView DataView ;
        readonly List<City> ListCity = new Data().Citys;
        readonly DispatcherTimer timer = new DispatcherTimer();
        public DispatcherTimer Timer => timer;
        public static MainWindow Me;
        public MainWindow()
        {
            InitializeComponent();
            Me = this;
            OnHor.IsChecked = Settings.Default.OnHor;//false
            //initialize NotifyIcon
            DataView = (DataView)FindResource("Data");
            B1.DataContext = DataView;
            ComboBoxCity.ItemsSource = ListCity;
            ComboBoxCity.DisplayMemberPath = "Name";
            ComboBoxCity.SelectionChanged += City_SelectionChanged;
            ComboBoxCity.SelectedIndex = Settings.Default.City;//qom
            //Text1.Text=SetDate();
            player.Stream = StreamMedia;
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            Clock.Show();
        }

        readonly ClockWindow Clock = new ClockWindow();
        DateTime Date;
        private void Timer_Tick(object sender, EventArgs e)
        {
            Date = DateTime.Now;
            TimeSpan time = new TimeSpan(Date.Hour, Date.Minute, Date.Second);
            if (time== DataView.Azansobh)
            {
                PlaySound();
            }
            else if (time == DataView.Azanzohr)
            {
                PlaySound();
            }
            else if (time == DataView.AzaneGorob)
            {
                PlaySound();
            }
        }

        readonly System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        readonly Stream StreamMedia =Properties.Resources.رحیم_موذن_زاده;// new MemoryStream
        void PlaySound()
        {
            player.Play();
        }
        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City d= ComboBoxCity.SelectedItem as City;
            Settings.Default.City = ComboBoxCity.SelectedIndex;
            SetDate(d);
        }

        //ابتدا یک آبجکت از کلاس تعریف می کنیم
        readonly Prayer_times_class.Prayer_times Prayer = new Prayer_times_class.Prayer_times();
        readonly System.Globalization.PersianCalendar PersianCalendar1 = new System.Globalization.PersianCalendar();


        //void SetDateOld(City city)
        //{
        //    Prayer prayer = new Prayer
        //    {
        //        //DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month,  DateTime.Now.Day,new System.Globalization.PersianCalendar());
        //        month = DateTime.Now.Month
        //    };
        //    byte Month = byte.Parse(DateTime.Now.Month.ToString());
        //    byte Day = byte.Parse(DateTime.Now.Day.ToString());
        //    DataView.Azansobh = prayer.MorningPrayer(Month, Day, city.Tol, city.Arz );
        //    DataView.Tolo = prayer.Sunrise(Month, (Day), city.Tol, city.Arz );
        //    DataView.Azanzohr = prayer.MiddayPrayer((Month), Day, city.Arz);
        //    DataView.Gorob = prayer.Sunset((Month), (Day), city.Tol, city.Arz);
        //    DataView.AzaneGorob = prayer.SunsetPrayer((Month), (Day), city.Tol, city.Arz);
        //    //lblNimehShab.Content = prayer.NimehShab();
        //}
        void SetDate(City city)
        {
            //طول و عرض جغرافیایی و ماه و روز را تنظیم می کنیم
            Prayer.SetGeo(city.Tol, city.Arz, PersianCalendar1.GetMonth(DateTime.Now), PersianCalendar1.GetDayOfMonth(DateTime.Now));

            TimeSpan d ;
            if (Settings.Default.OnHor)
            {
                d = new TimeSpan(1, 0, 0);
            }
            else
                d = new TimeSpan(0, 0, 0);
            DataView.Azansobh = TimeSpan.Parse(Prayer.GetAzanSobh())-d;
            DataView.Azanzohr = TimeSpan.Parse(Prayer.GetAzanZohr())-d;
            DataView.AzaneGorob = TimeSpan.Parse(Prayer.GetAzanMaghreb())-d;

            DataView.Tolo =TimeSpan.Parse( Prayer.GetTolue())-d;
            DataView.Gorob = TimeSpan.Parse(Prayer.GetGhorub())-d;
            DataView.NimehShab = TimeSpan.Parse(Prayer.GetNimehShab())-d;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void Settinge_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        private void Exit_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Clock.Close();
            Cansel = false;
            this.Close();
        }
        bool Cansel = true;

        

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Cansel)
                this.Hide();
            e.Cancel = Cansel;
        }

        private void MyNotifyIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                player.Stop();
            }
        }
        public static void ShowPopup()
        {
            FancyBalloon balloon = new FancyBalloon
            {
                DataContext = Me.DataView
            };
            Me.myNotifyIcon.ShowCustomBalloon(balloon, new System.Windows.Controls.Primitives.PopupAnimation(),5000);
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }

        private void ResetLockationClock_Click(object sender, RoutedEventArgs e)
        {
            Clock.Left = 0;
            Clock.Top = 0;
        }

        private void OnHor_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.OnHor= OnHor.IsChecked.Value;
            //set Chenge
            City d = ComboBoxCity.SelectedItem as City;
            Settings.Default.City = ComboBoxCity.SelectedIndex;
            SetDate(d);
        }

        private void OnHor_Unchecked(object sender, RoutedEventArgs e)
        {
            OnHor_Checked(sender, e);
        }

        private void Auto_Update_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool autoUpdate =! (sender as MenuItem).IsChecked;
            (sender as MenuItem).IsChecked = autoUpdate;
            App.TryToSetAutoUpdate(autoUpdate);
        }

        private void Update_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            App.TryToCheckUpdate();
        }
    }
}
