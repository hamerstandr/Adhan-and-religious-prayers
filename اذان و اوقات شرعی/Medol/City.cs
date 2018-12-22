namespace اذان_و_اوقات_شرعی
{
    public class City
    {
        private double tol;
        private double arz;
        private string name;

        public double Tol { get => tol; set => tol = value; }
        public double Arz { get => arz; set => arz = value; }
        public string Name { get => name; set => name = value; }

        public City(string Name, double Tol, double Arz)
        {
            this.Name = Name;
            this.Tol = Tol;
            this.Arz = Arz;
        }
    }
}
