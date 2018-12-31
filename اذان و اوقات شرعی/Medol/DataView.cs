using System;
using System.ComponentModel;
namespace اذان_و_اوقات_شرعی
{
    public class DataView : INotifyPropertyChanged
    {
        public TimeSpan Azansobh { get => _azansobh; set { _azansobh = value; NotifyPropertyChanged("Azansobh"); } }
        public TimeSpan Tolo { get => _tolo; set { _tolo = value; NotifyPropertyChanged("Tolo"); } }
        public TimeSpan Azanzohr { get => _azanzohr; set { _azanzohr = value; NotifyPropertyChanged("Azanzohr"); } }
        public TimeSpan Gorob { get => _gorob; set { _gorob = value; NotifyPropertyChanged("Gorob"); } }
        public TimeSpan AzaneGorob { get => _azaneGorob; set { _azaneGorob = value; NotifyPropertyChanged("AzaneGorob"); } }

        private TimeSpan _azansobh;
        private TimeSpan _tolo;
        private TimeSpan _azanzohr;
        private TimeSpan _gorob;
        private TimeSpan _azaneGorob;
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}