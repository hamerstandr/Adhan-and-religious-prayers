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
        readonly DataView Data = new DataView();
        public MainWindow()
        {
            InitializeComponent();
            G1.DataContext =Data;
            G2.DataContext = Data;
            var city=new object[] {
            "اراک",
            "اردبيل",
            "اروميه",
            "اصفهان",
            "اهواز",
            "ايلام",
            "بجنورد",
            "بندرعباس",
            "بوشهر",
            "بيرجند",
            "تبريز",
            "تهران",
            "خرم آباد",
            "رشت",
            "زاهدان",
            "زنجان",
            "ساري",
            "سمنان",
            "سنندج",
            "شهرکرد",
            "شيراز",
            "قزوين",
            "قم",
            "کرمان",
            "کرمانشاه",
            "گرگان",
            "مشهد",
            "همدان",
            "ياسوج",
            "يزد"};
            City.ItemsSource = city;
            City.SelectionChanged += City_SelectionChanged;
            City.SelectedIndex = Settings.Default.City;//qom
            //Text1.Text=SetDate();
        }
        double settol = 0;
        double setarz =0;
        readonly double[] tol = new double[30] { 49.7, 48.3, 45.07, 51.64, 48.68, 46.42, 57.33, 56.29, 50.84, 59.21, 46.28, 51.41, 48.34, 49.59, 60.86, 48.5, 53.06, 53.39, 47, 50.86, 52.52, 50, 50.88, 57.06, 47.09, 54.44, 59.58, 48.52, 51.59, 54.35 };
        readonly double[] arz = new double[30] { 34.09, 38.25, 37.55, 32.68, 31.32, 33.64, 37.47, 27.19, 28.97, 32.86, 38.08, 35.7, 33.46, 37.28, 29.5, 36.68, 36.57, 35.58, 35.31, 32.33, 29.62, 36.28, 34.64, 30.29, 34.34, 36.84, 36.31, 34.8, 30.67, 31.89 };

private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settol = tol[City.SelectedIndex];
            setarz = arz[City.SelectedIndex];
            Settings.Default.City = City.SelectedIndex;
            SetDate();
        }

        

        string SetDate()
        {
            string r = "";
            Prayer prayer = new Prayer();
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month,  DateTime.Now.Day,new System.Globalization.PersianCalendar());
            prayer.month = DateTime.Now.Month;
            byte Month = byte.Parse(DateTime.Now.Month.ToString());
            byte Day = byte.Parse(DateTime.Now.Day.ToString());
            Data.Azansobh = prayer.MorningPrayer(Month, Day, settol, setarz).ToString();
            Data.Tolo = prayer.Sunrise(Month, (Day), settol, setarz).ToString();
            Data.Azanzohr = prayer.MiddayPrayer((Month), (Day), settol).ToString()+"\r\n";
            Data.Gorob = prayer.Sunset((Month), (Day), settol, setarz).ToString();
            Data.AzaneGorob = prayer.SunsetPrayer((Month), (Day), settol, setarz).ToString();
            return r;
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
    struct City
    {
        public double Tol ;
        public double Arz ;
        public string Name;
        public City(string Name, double Tol, double Arz) : this()
        {
            this.Name = Name;
            this.Tol = Tol;
            this.Arz = Arz;
        }
    }
    class Data
    {//http://www.noojum.com/other/astronomy-tools/187-online-tools/6266-longitude-latitude.html
        public List<City> Citys;
        public Data()
        {
            Citys = new List<City>() {
                new City("اراک", 49.7, 34.09),
                new City("اردبيل", 48.3, 38.25),
                new City("اروميه", 45.07, 37.55),
                new City("اصفهان", 51.64, 32.68),
                new City("اهواز",  48.68,  31.32),
                new City("ايلام", 46.42, 33.64),
                new City("بجنورد",57.33, 37.47),
                new City("بندرعباس",  56.29, 27.19),
                new City("بوشهر", 50.84, 28.97),
                new City("بيرجند", 59.21, 32.86),
                new City("تبريز", 46.28, 35.7),
                new City("تهران", 51.41, 38.25),
                new City("خرم آباد", 48.34, 33.46),
                new City("رشت",  49.59,  37.28),
                new City("زاهدان",  60.86, 29.5),
                new City("زنجان", 48.5, 36.68),
                new City("ساري", 53.06, 36.57),
                new City("سمنان",  53.39, 35.58),
                new City("سنندج", 47, 35.31),
                new City("شهرکرد",  50.86, 32.33),
                new City("شيراز",  52.52, 29.62),
                new City("قزوين",  50, 36.28),
                new City("قم", 50.88, 34.64),
                new City("کرمان", 57.06, 30.29),
                new City("کرمانشاه", 48.3, 38.25),
                new City("گرگان", 47.09, 34.34),
                new City("مشهد", 54.44, 36.84),
                new City("همدان", 59.58, 36.31),
                new City("ياسوج", 51.59, 30.67),
                new City("يزد", 31.89,  54.35),
                new City("يزد", 31.89,  54.35),
            };

        }
    }
}
