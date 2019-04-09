using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace اذان_و_اوقات_شرعی.Service
{
    class SurroundingClass
    {
        public double Sal;
        public double Mah;
        public double Rooz;
        public double YearM;
        public double MonthM;
        public double DayM;
        public bool IfError;
        public bool Kabiseh;
        private bool NoErrorDateConvert;

        public void Tarikh(double Sal, double Mah, double Rooz)
        {
            IfError = false;
            Kabiseh = false;
            if (Rooz > 31 | Rooz < 1 | Mah > 12 | Mah < 1)
                IfError = true;
            if (Mah > 6 & Rooz == 31)
                IfError = true;
            if (Mah == 12 & (Sal + 1) % 4 != 0 & Rooz > 29)
                IfError = true;
            if ((IfError & NoErrorDateConvert == false))
            {
                MessageBox.Show("&Ecirc;&Ccedil;&Ntilde;&iacute;&Icirc; &atilde;&Uacute;&Ecirc;&Egrave;&Ntilde; &auml;&atilde;&iacute; &Egrave;&Ccedil;&Ocirc;&Iuml;");
                return;
            }

            if ((Sal + 1) % 4 == 0)
            {
                Kabiseh = true;
                switch (Mah)
                {
                    case 1:
                        {
                            if (Rooz < 13)
                            {
                                DayM = Rooz + 19; MonthM = 3; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 12; MonthM = 4; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 2:
                        {
                            if (Rooz < 12)
                            {
                                DayM = Rooz + 19; MonthM = 4; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 11; MonthM = 5; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 3:
                        {
                            if (Rooz < 12)
                            {
                                DayM = Rooz + 20; MonthM = 5; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 11; MonthM = 6; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 4:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 20; MonthM = 6; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 7; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 5:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 21; MonthM = 7; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 8; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 6:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 21; MonthM = 8; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 9; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 7:
                        {
                            if (Rooz < 10)
                            {
                                DayM = Rooz + 21; MonthM = 9; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 9; MonthM = 10; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 8:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 21; MonthM = 10; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 11; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 9:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 20; MonthM = 11; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 12; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 10:
                        {
                            if (Rooz < 12)
                            {
                                DayM = Rooz + 20; MonthM = 12; YearM = Sal + 621;
                            }
                            else if (Rooz == 12)
                            {
                                DayM = 1; MonthM = 1; YearM = Sal + 622;
                            }
                            else
                            {
                                DayM = Rooz - 11; MonthM = 1; YearM = Sal + 622;
                            }

                            break;
                        }

                    case 11:
                        {
                            if (Rooz < 13)
                            {
                                DayM = Rooz + 19; MonthM = 1; YearM = Sal + 622;
                            }
                            else
                            {
                                DayM = Rooz - 12; MonthM = 2; YearM = Sal + 622;
                            }

                            break;
                        }

                    case 12:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 18; MonthM = 2; YearM = Sal + 622;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 3; YearM = Sal + 622;
                            }

                            break;
                        }
                }
            }
            else
                switch (Mah)
                {
                    case 1:
                        {
                            if (Rooz < 12)
                            {
                                DayM = Rooz + 20; MonthM = 3; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 11; MonthM = 4; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 2:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 20; MonthM = 4; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 5; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 3:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 21; MonthM = 5; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 6; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 4:
                        {
                            if (Rooz < 10)
                            {
                                DayM = Rooz + 21; MonthM = 6; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 9; MonthM = 7; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 5:
                        {
                            if (Rooz < 10)
                            {
                                DayM = Rooz + 22; MonthM = 7; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 9; MonthM = 8; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 6:
                        {
                            if (Rooz < 10)
                            {
                                DayM = Rooz + 22; MonthM = 8; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 9; MonthM = 9; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 7:
                        {
                            if (Rooz < 9)
                            {
                                DayM = Rooz + 22; MonthM = 9; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 8; MonthM = 10; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 8:
                        {
                            if (Rooz < 10)
                            {
                                DayM = Rooz + 22; MonthM = 10; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 9; MonthM = 11; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 9:
                        {
                            if (Rooz < 10)
                            {
                                DayM = Rooz + 21; MonthM = 11; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 9; MonthM = 12; YearM = Sal + 621;
                            }

                            break;
                        }

                    case 10:
                        {
                            if (Rooz < 11)
                            {
                                DayM = Rooz + 21; MonthM = 12; YearM = Sal + 621;
                            }
                            else
                            {
                                DayM = Rooz - 10; MonthM = 1; YearM = Sal + 622;
                            }

                            break;
                        }

                    case 11:
                        {
                            if (Rooz < 12)
                            {
                                DayM = Rooz + 20; MonthM = 1; YearM = Sal + 622;
                            }
                            else
                            {
                                DayM = Rooz - 11; MonthM = 2; YearM = Sal + 622;
                            }

                            break;
                        }

                    case 12:
                        {
                            if ((Math.Abs(1380 - Sal)) % 4 == 0)
                            {
                                if (Rooz < 10)
                                {
                                    DayM = Rooz + 19; MonthM = 2; YearM = Sal + 622;
                                }
                                else
                                {
                                    DayM = Rooz - 9; MonthM = 3; YearM = Sal + 622;
                                }
                            }
                            else if ((Math.Abs(1381 - Sal)) % 4 == 0)
                            {
                                if (Rooz < 10)
                                {
                                    DayM = Rooz + 19; MonthM = 2; YearM = Sal + 622;
                                }
                                else
                                {
                                    DayM = Rooz - 9; MonthM = 3; YearM = Sal + 622;
                                }
                            }
                            else if ((Math.Abs(1382 - Sal)) % 4 == 0)
                            {
                                if (Rooz < 11)
                                {
                                    DayM = Rooz + 19; MonthM = 2; YearM = Sal + 622;
                                }
                                else
                                {
                                    DayM = Rooz - 10; MonthM = 3; YearM = Sal + 622;
                                }
                            }
                            break;
                        }
                }

            return;
            
        }

        public void M2H(double Year, double month, double day)
        {
            switch (month)
            {
                case 1:
                    {
                        if ((Year - 1) % 4 == 0)
                        {
                            if (day < 20)
                            {
                                Rooz = day + 11; Mah = 10; Sal = Year - 622;
                            }
                            else
                            {
                                Rooz = day - 19; Mah = 11; Sal = Year - 622;
                            }
                        }
                        else if (day < 21)
                        {
                            Rooz = day + 10; Mah = 10; Sal = Year - 622;
                        }
                        else
                        {
                            Rooz = day - 20; Mah = 11; Sal = Year - 622;
                        }

                        break;
                    }

                case 2:
                    {
                        if ((Year - 1) % 4 == 0)
                        {
                            if (day < 19)
                            {
                                Rooz = day + 12; Mah = 11; Sal = Year - 622;
                            }
                            else
                            {
                                Rooz = day - 18; Mah = 12; Sal = Year - 622;
                            }
                        }
                        else if (day < 20)
                        {
                            Rooz = day + 11; Mah = 11; Sal = Year - 622;
                        }
                        else
                        {
                            Rooz = day - 19; Mah = 12; Sal = Year - 622;
                        }

                        break;
                    }

                case 3:
                    {
                        if (((Year) % 4 == 0))
                        {
                            if (day < 20)
                            {
                                Rooz = day + 10; Mah = 12; Sal = Year - 622;
                            }
                            else
                            {
                                Rooz = day - 19; Mah = 1; Sal = Year - 621;
                            }
                        }
                        else if (((Year) % 2 == 0))
                        {
                            if (day < 21)
                            {
                                Rooz = day + 9; Mah = 12; Sal = Year - 622;
                            }
                            else
                            {
                                Rooz = day - 20; Mah = 1; Sal = Year - 621;
                            }
                        }
                        else
                        {
                            if (((Year + Year - 1) / (double)2) % 4 == 0)
                            {
                                if (day < 21)
                                {
                                    Rooz = day + 10; Mah = 12; Sal = Year - 622;
                                }
                                else
                                {
                                    Rooz = day - 20; Mah = 1; Sal = Year - 621;
                                }
                            }

                            if ((Year + Year - 1) /2 % 4 != 0)
                            {
                                if (day < 21)
                                {
                                    Rooz = day + 9; Mah = 12; Sal = Year - 622;
                                }
                                else
                                {
                                    Rooz = day - 20; Mah = 1; Sal = Year - 621;
                                }
                            }
                        }

                        break;
                    }

                case 4:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 20)
                            {
                                Rooz = day + 12; Mah = 1; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 19; Mah = 2; Sal = Year - 621;
                            }
                        }
                        else if (day < 21)
                        {
                            Rooz = day + 11; Mah = 1; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 20; Mah = 2; Sal = Year - 621;
                        }

                        break;
                    }

                case 5:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 21)
                            {
                                Rooz = day + 11; Mah = 2; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 20; Mah = 3; Sal = Year - 621;
                            }
                        }
                        else if (day < 22)
                        {
                            Rooz = day + 10; Mah = 2; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 21; Mah = 3; Sal = Year - 621;
                        }

                        break;
                    }

                case 6:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 21)
                            {
                                Rooz = day + 11; Mah = 3; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 20; Mah = 4; Sal = Year - 621;
                            }
                        }
                        else if (day < 22)
                        {
                            Rooz = day + 10; Mah = 3; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 21; Mah = 4; Sal = Year - 621;
                        }

                        break;
                    }

                case 7:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 22)
                            {
                                Rooz = day + 10; Mah = 4; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 21; Mah = 5; Sal = Year - 621;
                            }
                        }
                        else if (day < 23)
                        {
                            Rooz = day + 9; Mah = 4; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 22; Mah = 5; Sal = Year - 621;
                        }

                        break;
                    }

                case 8:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 22)
                            {
                                Rooz = day + 10; Mah = 5; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 21; Mah = 6; Sal = Year - 621;
                            }
                        }
                        else if (day < 23)
                        {
                            Rooz = day + 9; Mah = 5; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 22; Mah = 6; Sal = Year - 621;
                        }

                        break;
                    }

                case 9:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 22)
                            {
                                Rooz = day + 10; Mah = 6; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 21; Mah = 7; Sal = Year - 621;
                            }
                        }
                        else if (day < 23)
                        {
                            Rooz = day + 9; Mah = 6; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 22; Mah = 7; Sal = Year - 621;
                        }

                        break;
                    }

                case 10:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 22)
                            {
                                Rooz = day + 9; Mah = 7; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 21; Mah = 8; Sal = Year - 621;
                            }
                        }
                        else if (day < 23)
                        {
                            Rooz = day + 8; Mah = 7; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 22; Mah = 8; Sal = Year - 621;
                        }

                        break;
                    }

                case 11:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 21)
                            {
                                Rooz = day + 10; Mah = 8; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 20; Mah = 9; Sal = Year - 621;
                            }
                        }
                        else if (day < 22)
                        {
                            Rooz = day + 9; Mah = 8; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 21; Mah = 9; Sal = Year - 621;
                        }

                        break;
                    }

                case 12:
                    {
                        if ((Year) % 4 == 0)
                        {
                            if (day < 21)
                            {
                                Rooz = day + 10; Mah = 9; Sal = Year - 621;
                            }
                            else
                            {
                                Rooz = day - 20; Mah = 10; Sal = Year - 621;
                            }
                        }
                        else if (day < 22)
                        {
                            Rooz = day + 9; Mah = 9; Sal = Year - 621;
                        }
                        else
                        {
                            Rooz = day - 21; Mah = 10; Sal = Year - 621;
                        }

                        break;
                    }
            }

            Sal = double.Parse((Sal).ToString().PadRight(2));
        }
    }
}
