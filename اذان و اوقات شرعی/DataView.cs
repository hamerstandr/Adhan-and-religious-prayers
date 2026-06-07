namespace اذان_و_اوقات_شرعی
{
    internal class DataView : System.ComponentModel.INotifyPropertyChanged
    {
        private string _azansobh;
        private string _tolo;
        private string _azanzohr;
        private string _gorob;
        private string _azaneGorob;
        private string _nimehShab;
        private string _persianDate;
        private string _gregorianDate;
        private string _lunarDate;
        private string _events;

        public string Azansobh 
        { 
            get => _azansobh; 
            set { _azansobh = value; OnPropertyChanged(nameof(Azansobh)); }
        }
        public string Tolo 
        { 
            get => _tolo; 
            set { _tolo = value; OnPropertyChanged(nameof(Tolo)); }
        }
        public string Azanzohr 
        { 
            get => _azanzohr; 
            set { _azanzohr = value; OnPropertyChanged(nameof(Azanzohr)); }
        }
        public string Gorob 
        { 
            get => _gorob; 
            set { _gorob = value; OnPropertyChanged(nameof(Gorob)); }
        }
        public string AzaneGorob 
        { 
            get => _azaneGorob; 
            set { _azaneGorob = value; OnPropertyChanged(nameof(AzaneGorob)); }
        }
        public string NimehShab 
        { 
            get => _nimehShab; 
            set { _nimehShab = value; OnPropertyChanged(nameof(NimehShab)); }
        }
        public string PersianDate 
        { 
            get => _persianDate; 
            set { _persianDate = value; OnPropertyChanged(nameof(PersianDate)); }
        }
        public string GregorianDate 
        { 
            get => _gregorianDate; 
            set { _gregorianDate = value; OnPropertyChanged(nameof(GregorianDate)); }
        }
        public string LunarDate 
        { 
            get => _lunarDate; 
            set { _lunarDate = value; OnPropertyChanged(nameof(LunarDate)); }
        }
        public string Events 
        { 
            get => _events; 
            set { _events = value; OnPropertyChanged(nameof(Events)); }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}