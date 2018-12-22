using System.ComponentModel;
namespace اذان_و_اوقات_شرعی
{
    public class DataView : INotifyPropertyChanged
    {
        private string _azansobh = "";

        public string Azansobh { get => _azansobh; set { _azansobh = value; NotifyPropertyChanged("Azansobh"); } }
        public string Tolo { get => _tolo; set { _tolo = value; NotifyPropertyChanged("Tolo"); } }
        public string Azanzohr { get => _azanzohr; set { _azanzohr = value; NotifyPropertyChanged("Azanzohr"); } }
        public string Gorob { get => _gorob; set { _gorob = value; NotifyPropertyChanged("Gorob"); } }
        public string AzaneGorob { get => _azaneGorob; set { _azaneGorob = value; NotifyPropertyChanged("AzaneGorob"); } }

        
        private string _tolo;
        private string _azanzohr;
        private string _gorob;
        private string _azaneGorob;
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}