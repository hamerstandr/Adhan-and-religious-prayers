using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdhanPrayerTimes.Models
{
    public class PrayerTimeViewModel : INotifyPropertyChanged
    {
        private string _fajr = "";
        private string _sunrise = "";
        private string _dhuhr = "";
        private string _sunset = "";
        private string _maghrib = "";
        private string _midnight = "";
        private string _persianDate = "";
        private string _gregorianDate = "";
        private string _lunarDate = "";
        private string _events = "";
        private string _selectedCity = "";

        public string Fajr
        {
            get => _fajr;
            set { _fajr = value; OnPropertyChanged(); }
        }

        public string Sunrise
        {
            get => _sunrise;
            set { _sunrise = value; OnPropertyChanged(); }
        }

        public string Dhuhr
        {
            get => _dhuhr;
            set { _dhuhr = value; OnPropertyChanged(); }
        }

        public string Sunset
        {
            get => _sunset;
            set { _sunset = value; OnPropertyChanged(); }
        }

        public string Maghrib
        {
            get => _maghrib;
            set { _maghrib = value; OnPropertyChanged(); }
        }

        public string Midnight
        {
            get => _midnight;
            set { _midnight = value; OnPropertyChanged(); }
        }

        public string PersianDate
        {
            get => _persianDate;
            set { _persianDate = value; OnPropertyChanged(); }
        }

        public string GregorianDate
        {
            get => _gregorianDate;
            set { _gregorianDate = value; OnPropertyChanged(); }
        }

        public string LunarDate
        {
            get => _lunarDate;
            set { _lunarDate = value; OnPropertyChanged(); }
        }

        public string Events
        {
            get => _events;
            set { _events = value; OnPropertyChanged(); }
        }

        public string SelectedCity
        {
            get => _selectedCity;
            set { _selectedCity = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class City
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public City(string name, double longitude, double latitude)
        {
            Name = name;
            Longitude = longitude;
            Latitude = latitude;
        }

        public override string ToString() => Name;
    }
}
