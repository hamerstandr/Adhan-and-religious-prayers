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
        public DataView DataView = new DataView();
        List<City> ListCity = new Data().Citys;
        readonly DispatcherTimer timer = new DispatcherTimer();
        public DispatcherTimer Timer => timer;
        public MainWindow()
        {
            InitializeComponent();
            G1.DataContext = DataView;
            G2.DataContext = DataView;
            ComboBoxCity.ItemsSource = ListCity;
            ComboBoxCity.DisplayMemberPath = "Name";
            ComboBoxCity.SelectionChanged += City_SelectionChanged;
            ComboBoxCity.SelectedIndex = Settings.Default.City;//qom
            //Text1.Text=SetDate();
            player.Stream = StreamMedia;
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }
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
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        Stream StreamMedia =Properties.Resources.رحیم_موذن_زاده;// new MemoryStream
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

        

        void SetDate(City city)
        {
            Prayer prayer = new Prayer();
            //DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month,  DateTime.Now.Day,new System.Globalization.PersianCalendar());
            prayer.month = DateTime.Now.Month;
            byte Month = byte.Parse(DateTime.Now.Month.ToString());
            byte Day = byte.Parse(DateTime.Now.Day.ToString());
            DataView.Azansobh = prayer.MorningPrayer(Month, Day, city.Tol, city.Arz );
            DataView.Tolo = prayer.Sunrise(Month, (Day), city.Tol, city.Arz );
            DataView.Azanzohr = prayer.MiddayPrayer((Month), Day, city.Arz);
            DataView.Gorob = prayer.Sunset((Month), (Day), city.Tol, city.Arz);
            DataView.AzaneGorob = prayer.SunsetPrayer((Month), (Day), city.Tol, city.Arz);
            //lblNimehShab.Content = prayer.NimehShab();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
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
    }
}
