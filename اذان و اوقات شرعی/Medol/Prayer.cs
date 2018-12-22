using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace اذان_و_اوقات_شرعی
{
    public class Prayer
    {

        //Longitude=طول
        //Latitude=عرض
        struct Values
        {
            public double Val0, Val1;
        }
        public int month;
        public TimeSpan MorningPrayer(byte Month, byte Day, double Longitude, double Latitude)
        {
            Values ep = Sun(Month, Day, 4, Longitude);
            double zr = ep.Val0,
                delta = ep.Val1,
                ha = Loc2hor(108.0, delta, Latitude),
                t = Round(zr - ha, 24);
            ep = Sun(Month, Day, t, Longitude);
            zr = ep.Val0;
            delta = ep.Val1;
            ha = Loc2hor(108.0, delta, Latitude);
            t = Round(zr - ha, 24);
            return TimeSpan.Parse(Hms(t));
        }
        public TimeSpan Sunrise(byte Month, byte Day, double Longitude, double Latitude)
        {
            Values ep = Sun(Month, Day, 6, Longitude);
            double zr = ep.Val0,
            delta = ep.Val1,
            ha = Loc2hor(90.833, delta, Latitude),
            t = Round(zr - ha, 24);
            ep = Sun(Month, Day, t, Longitude);
            zr = ep.Val0;
            delta = ep.Val1;
            ha = Loc2hor(90.833, delta, Latitude);
            t = Round(zr - ha, 24);
            return TimeSpan.Parse(Hms(t));
        }
        public TimeSpan MiddayPrayer(byte Month, byte Day, double Longitude)
        {
            Values ep = Sun(Month, Day, 12, Longitude);
            ep = Sun(Month, Day, ep.Val0, Longitude);
            double zr = ep.Val0;
            return TimeSpan.Parse(Hms(zr));
        }
        public TimeSpan Sunset(byte Month, byte Day, double Longitude, double Latitude)
        {
            Values ep = Sun(Month, Day, 18, Longitude);
            double zr = ep.Val0,
            delta = ep.Val1,
            ha = Loc2hor(90.833, delta, Latitude),
            t = Round(zr + ha, 24);
            ep = Sun(Month, Day, t, Longitude);
            zr = ep.Val0;
            delta = ep.Val1;
            ha = Loc2hor(90.833, delta, Latitude);
            t = Round(zr + ha, 24);
            return TimeSpan.Parse(Hms(t));
        }
        public TimeSpan SunsetPrayer(byte Month, byte Day, double Longitude, double Latitude)
        {
            Values ep = Sun(Month, Day, 18.5, Longitude);
            double zr = ep.Val0,
            delta = ep.Val1,
            ha = Loc2hor(94.3, delta, Latitude),
            t = Round(zr + ha, 24);
            ep = Sun(Month, Day, t, Longitude);
            zr = ep.Val0;
            delta = ep.Val1;
            ha = Loc2hor(94.3, delta, Latitude);
            t = Round(zr + ha, 24);
            return TimeSpan.Parse(Hms(t));
        }

        Values Sun(byte m, double d, double h, double lg)
        {
            if (m < 7)
                d = 31 * (m - 1) + d + h / 24;
            else
                d = 6 + 30 * (m - 1) + d + h / 24;
            double M = 74.2023 + 0.98560026 * d,
                L = -2.75043 + 0.98564735 * d,
                lst = 8.3162159 + 0.065709824 * Math.Floor(d) + 1.00273791 * 24 * (d % 1) + lg / 15,
                e = 0.0167065,
                omega = 4.85131 - 0.052954 * d,
                ep = 23.4384717 + 0.00256 * Cosd(omega),
                ed = 180.0 / Math.PI * e, u = M;
            for (byte i = 1; i < 5; i++)
                u = u - (u - ed * Sind(u) - M) / (1 - e * Cosd(u));
            double v = 2 * Atand(Tand(u / 2) * Math.Sqrt((1 + e) / (1 - e))),
                theta = L + v - M - 0.00569 - 0.00479 * Sind(omega),
                delta = Asind(Sind(ep) * Sind(theta)),
                alpha = 180.0 / Math.PI * Math.Atan2(Cosd(ep) * Sind(theta), Cosd(theta));
            if (alpha >= 360)
                alpha -= 360;
            double ha = lst - alpha / 15;
            double zr = Round(h - ha, 24);
            Values vlu;
            vlu.Val1 = delta;
            vlu.Val0 = zr;
            return vlu;
        }
        double Sind(double x) { return Math.Sin(Math.PI / 180.0 * x); }
        double Cosd(double x) { return Math.Cos(Math.PI / 180.0 * x); }
        double Tand(double x) { return Math.Tan(Math.PI / 180.0 * x); }
        double Atand(double x) { return Math.Atan(x) * 180.0 / Math.PI; }
        double Asind(double x) { return Math.Asin(x) * 180.0 / Math.PI; }
        double Acosd(double x) { return Math.Acos(x) * 180.0 / Math.PI; }
        double Loc2hor(double z, double d, double p) { return Acosd((Cosd(z) - Sind(d) * Sind(p)) / Cosd(d) / Cosd(p)) / 15; }
        double Round(double x, byte a)
        {
            double tmp = x % a;
            if (tmp < 0)
                tmp += a;
            return tmp;
        }
        string Hms(double x)
        {
            x = Math.Floor(3600 * x);
            double
            h = Math.Floor(x / 3600),
            mp = x - 3600 * h,
            m = Math.Floor(mp / 60),
            s = Math.Floor(mp - 60 * m);
            //چون ساعت جدید میشود
            if (month < 7)
                h++;
            //
            return h.ToString() + ":" + m.ToString() + ":" + s.ToString();
        }


    }

}
