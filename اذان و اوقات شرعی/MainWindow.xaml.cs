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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month,  DateTime.Now.Day,new System.Globalization.PersianCalendar());
            prayer.month = DateTime.Now.Month;
            byte Month = byte.Parse(DateTime.Now.Month.ToString());
            byte Day = byte.Parse(DateTime.Now.Day.ToString());
            DataView.Azansobh = prayer.MorningPrayer(Month, Day, city.Arz, city.Tol).ToString();
            DataView.Tolo = prayer.Sunrise(Month, (Day), city.Arz, city.Tol).ToString();
            DataView.Azanzohr = prayer.MiddayPrayer((Month), Day, city.Arz).ToString()+"\r\n";
            DataView.Gorob = prayer.Sunset((Month), (Day), city.Arz, city.Tol).ToString();
            DataView.AzaneGorob = prayer.SunsetPrayer((Month), (Day), city.Arz, city.Tol).ToString();
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
    }
}
