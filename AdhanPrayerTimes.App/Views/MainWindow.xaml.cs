using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AdhanPrayerTimes.Models;
using AdhanPrayerTimes.Services;

namespace AdhanPrayerTimes.Views
{
    public partial class MainWindow : Window
    {
        private readonly PrayerTimeViewModel _viewModel;
        private readonly PrayerTimeService _prayerService;
        private readonly CityService _cityService;
        private readonly PersianCalendarEventsService _calendarService;
        private readonly System.Windows.Threading.DispatcherTimer _timer;
        private System.Media.SoundPlayer? _soundPlayer;

        public MainWindow()
        {
            InitializeComponent();
            
            // مقداردهی سرویس‌ها
            _viewModel = new PrayerTimeViewModel();
            _prayerService = new PrayerTimeService();
            _cityService = new CityService();
            _calendarService = new PersianCalendarEventsService();
            
            // تنظیم DataContext
            DataContext = _viewModel;
            
            // بارگذاری شهرها
            ComboBoxCity.ItemsSource = _cityService.GetCities();
            ComboBoxCity.SelectedIndex = 29; // پیش‌فرض: قم
            
            // تنظیم تایمر
            _timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
            
            // بارگذاری داده‌های تقویم
            LoadCalendarData();
            
            // به‌روزرسانی اوقات شرعی
            UpdatePrayerTimes();
            
            // بارگذاری صدا
            try
            {
                // در نسخه نهایی می‌توان فایل صوتی را از Resources بارگذاری کرد
                // _soundPlayer = new System.Media.SoundPlayer(Properties.Resources.Azan);
            }
            catch { }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var currentTime = new TimeSpan(now.Hour, now.Minute, now.Second);
            
            // بررسی زمان اذان
            if (currentTime.Hours == int.Parse(_viewModel.Fajr.Split(':')[0]) &&
                currentTime.Minutes == int.Parse(_viewModel.Fajr.Split(':')[1]) &&
                currentTime.Seconds == 0)
            {
                PlayAzan();
            }
            else if (currentTime.Hours == int.Parse(_viewModel.Dhuhr.Split(':')[0]) &&
                     currentTime.Minutes == int.Parse(_viewModel.Dhuhr.Split(':')[1]) &&
                     currentTime.Seconds == 0)
            {
                PlayAzan();
            }
            else if (currentTime.Hours == int.Parse(_viewModel.Maghrib.Split(':')[0]) &&
                     currentTime.Minutes == int.Parse(_viewModel.Maghrib.Split(':')[1]) &&
                     currentTime.Seconds == 0)
            {
                PlayAzan();
            }
            
            // به‌روزرسانی ساعت هر دقیقه
            if (currentTime.Seconds == 0)
            {
                UpdateClockDisplay();
            }
        }

        private void PlayAzan()
        {
            try
            {
                _soundPlayer?.Play();
                
                // نمایش پیام
                MessageBox.Show("وقت نماز فرا رسید!", "اذان", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch { }
        }

        private void UpdateClockDisplay()
        {
            var now = DateTime.Now;
            var persianCalendar = new System.Globalization.PersianCalendar();
            
            // تاریخ شمسی
            var persianDate = $"{persianCalendar.GetYear(now)}/{persianCalendar.GetMonth(now):D2}/{persianCalendar.GetDayOfMonth(now):D2}";
            var dayOfWeek = GetPersianDayOfWeek(persianCalendar.GetDayOfWeek(now));
            _viewModel.PersianDate = $"{persianDate} - {dayOfWeek}";
            
            // تاریخ میلادی
            _viewModel.GregorianDate = $"{now.Year}/{now.Month:D2}/{now.Day:D2}";
            
            // تاریخ قمری
            _viewModel.LunarDate = GetLunarDate(now);
            
            // رویداد امروز
            var todayEvent = _calendarService.GetTodayEvent(persianCalendar.GetMonth(now), persianCalendar.GetDayOfMonth(now));
            _viewModel.Events = todayEvent ?? "";
        }

        private string GetPersianDayOfWeek(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Saturday => "شنبه",
                DayOfWeek.Sunday => "یکشنبه",
                DayOfWeek.Monday => "دوشنبه",
                DayOfWeek.Tuesday => "سه‌شنبه",
                DayOfWeek.Wednesday => "چهارشنبه",
                DayOfWeek.Thursday => "پنجشنبه",
                DayOfWeek.Friday => "جمعه",
                _ => ""
            };
        }

        private string GetLunarDate(DateTime date)
        {
            // محاسبه تقریبی تاریخ قمری
            var persianCalendar = new System.Globalization.PersianCalendar();
            int py = persianCalendar.GetYear(date);
            int pm = persianCalendar.GetMonth(date);
            int pd = persianCalendar.GetDayOfMonth(date);
            
            int jy = py;
            int jm = pm;
            int jd = pd;
            
            if (jm > 6)
                jm -= 6;
            else
            {
                jy -= 1;
                jm += 6;
            }
            
            int jDay = jd + ((jm - 1) * 30) + (jy * 365) + ((jy / 4) - (jy / 100) + (jy / 400)) + 1948320;
            double lunarAge = ((jDay - 1948084) % 29.530588);
            if (lunarAge < 0) lunarAge += 29.530588;
            
            int lunarMonth = (int)((lunarAge / 29.530588) * 12) + 1;
            if (lunarMonth > 12) lunarMonth = 1;
            
            int lunarDay = (int)lunarAge + 1;
            if (lunarDay > 30) lunarDay = 1;
            
            string[] lunarMonthNames = { "محرم", "صفر", "ربیع‌الاول", "ربیع‌الثانی", 
                                        "جمادی‌الاول", "جمادی‌الثانی", "رجب", "شعبان", 
                                        "رمضان", "شوال", "ذیقعده", "ذیحجه" };
            
            int lunarYear = (int)((py * 0.97) + 622);
            
            return $"{lunarDay} {lunarMonthNames[lunarMonth - 1]} {lunarYear}";
        }

        private void LoadCalendarData()
        {
            var now = DateTime.Now;
            var persianCalendar = new System.Globalization.PersianCalendar();
            var persianYear = persianCalendar.GetYear(now);
            var persianMonth = persianCalendar.GetMonth(now);
            
            UpdateClockDisplay();
        }

        private void UpdatePrayerTimes()
        {
            if (ComboBoxCity.SelectedItem is City selectedCity)
            {
                var persianCalendar = new System.Globalization.PersianCalendar();
                var now = DateTime.Now;
                
                _prayerService.SetLocation(
                    selectedCity.Longitude,
                    selectedCity.Latitude,
                    persianCalendar.GetMonth(now),
                    persianCalendar.GetDayOfMonth(now));
                
                _viewModel.Fajr = _prayerService.GetFajr();
                _viewModel.Sunrise = _prayerService.GetSunrise();
                _viewModel.Dhuhr = _prayerService.GetDhuhr();
                _viewModel.Sunset = _prayerService.GetSunset();
                _viewModel.Maghrib = _prayerService.GetMaghrib();
                _viewModel.Midnight = _prayerService.GetMidnight();
                _viewModel.SelectedCity = selectedCity.Name;
            }
        }

        private void ComboBoxCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePrayerTimes();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            // ذخیره تنظیمات
            MessageBox.Show("تنظیمات با موفقیت ذخیره شد.", "پیام", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                DragMove();
            }
        }
    }
}
