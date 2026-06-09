using System;
using System.Collections.Generic;

namespace AdhanPrayerTimes.Services
{
    /// <summary>
    /// سرویس محاسبه اوقات شرعی با استفاده از الگوریتم‌های نجومی
    /// </summary>
    public class PrayerTimeService
    {
        private double _longitude;
        private double _latitude;
        private int _month;
        private int _day;

        public void SetLocation(double longitude, double latitude, int month, int day)
        {
            _longitude = longitude;
            _latitude = latitude;
            _month = month;
            _day = day;
        }

        public string GetFajr()
        {
            return CalculatePrayerTime(PrayerType.Fajr);
        }

        public string GetSunrise()
        {
            return CalculatePrayerTime(PrayerType.Sunrise);
        }

        public string GetDhuhr()
        {
            return CalculatePrayerTime(PrayerType.Dhuhr);
        }

        public string GetSunset()
        {
            return CalculatePrayerTime(PrayerType.Sunset);
        }

        public string GetMaghrib()
        {
            return CalculatePrayerTime(PrayerType.Maghrib);
        }

        public string GetMidnight()
        {
            return CalculatePrayerTime(PrayerType.Midnight);
        }

        private enum PrayerType
        {
            Fajr,
            Sunrise,
            Dhuhr,
            Sunset,
            Maghrib,
            Midnight
        }

        private string CalculatePrayerTime(PrayerType type)
        {
            // الگوریتم ساده شده برای محاسبه اوقات شرعی
            // در نسخه کامل می‌توان از کتابخانه‌های تخصصی استفاده کرد
            
            var date = new DateTime(DateTime.Now.Year, _month, _day);
            var dayOfYear = date.DayOfYear;
            
            // محاسبات تقریبی بر اساس روز سال و موقعیت جغرافیایی
            double baseTime = 12.0 + (_longitude / 15.0); // زمان ظهر شرعی
            
            double equationOfTime = -7.6 * Math.Sin(0.0327 * dayOfYear) - 9.9 * Math.Cos(0.0327 * dayOfYear);
            double declination = 23.45 * Math.Sin(0.0172 * (dayOfYear - 80));
            
            double hourAngle = Math.Acos((Math.Cos(Math.PI * 90.833 / 180) - 
                Math.Sin(Math.PI * _latitude / 180) * Math.Sin(Math.PI * declination / 180)) /
                (Math.Cos(Math.PI * _latitude / 180) * Math.Cos(Math.PI * declination / 180)));
            
            double sunriseOffset = hourAngle * 180 / Math.PI / 15.0;
            
            switch (type)
            {
                case PrayerType.Fajr:
                    return FormatTime(baseTime - sunriseOffset - 1.5 + equationOfTime / 60.0);
                case PrayerType.Sunrise:
                    return FormatTime(baseTime - sunriseOffset + equationOfTime / 60.0);
                case PrayerType.Dhuhr:
                    return FormatTime(baseTime + equationOfTime / 60.0);
                case PrayerType.Sunset:
                    return FormatTime(baseTime + sunriseOffset + equationOfTime / 60.0);
                case PrayerType.Maghrib:
                    return FormatTime(baseTime + sunriseOffset + 0.3 + equationOfTime / 60.0);
                case PrayerType.Midnight:
                    return FormatTime(baseTime + 12.0 + equationOfTime / 60.0);
                default:
                    return "";
            }
        }

        private string FormatTime(double decimalTime)
        {
            int hours = (int)Math.Floor(decimalTime);
            int minutes = (int)Math.Round((decimalTime - hours) * 60);
            
            if (hours < 0) hours += 24;
            if (hours >= 24) hours -= 24;
            if (minutes >= 60)
            {
                hours++;
                minutes -= 60;
            }
            
            return $"{hours:D2}:{minutes:D2}";
        }
    }

    /// <summary>
    /// سرویس دریافت رویدادهای تقویم فارسی
    /// </summary>
    public class PersianCalendarEventsService
    {
        private static readonly Dictionary<string, string> HolidayEvents = new()
        {
            { "1/1", "نوروز" },
            { "1/2", "عید نوروز" },
            { "1/3", "عید نوروز" },
            { "1/4", "عید نوروز" },
            { "1/12", "روز جمهوری اسلامی" },
            { "1/13", "سیزده به در" },
            { "2/12", "شهادت امام علی (ع)" },
            { "3/14", "رحلت امام خمینی" },
            { "3/15", "قیام 15 خرداد" },
            { "4/14", "شهادت امام رضا (ع)" },
            { "5/9", "سالگرد عملیات مرصاد" },
            { "6/31", "آغاز جنگ تحمیلی" },
            { "7/7", "شهادت امام محمد باقر (ع)" },
            { "7/13", "تولید امام حسن عسکری (ع)" },
            { "7/20", "اربعین حسینی" },
            { "7/28", "رحلت پیامبر اکرم (ص)" },
            { "8/8", "شهادت امام حسن عسکری (ع)" },
            { "8/17", "میلاد پیامبر اکرم (ص)" },
            { "9/14", "شب قدر" },
            { "9/16", "شب قدر" },
            { "9/18", "شب قدر" },
            { "9/19", "عید فطر" },
            { "9/20", "عید فطر" },
            { "9/21", "عید فطر" },
            { "9/25", "شهادت امام جعفر صادق (ع)" },
            { "10/25", "عید قربان" },
            { "11/1", "عید غدیر خم" },
            { "11/10", "عید مبعث" },
            { "11/15", "ولادت امام حسین (ع)" },
            { "11/20", "ولادت امام سجاد (ع)" },
            { "11/25", "ولادت امام باقر (ع)" },
            { "12/1", "ولادت امام رضا (ع)" },
            { "12/10", "ولادت امام محمد تقی (ع)" },
            { "12/18", "ولادت امام علی النقی (ع)" },
            { "12/23", "ولادت امام حسن عسکری (ع)" },
            { "12/29", "ولادت حضرت مهدی (عج)" }
        };

        public string[] GetMonthEvents(int persianYear, int persianMonth)
        {
            var events = new List<string>();
            
            for (int day = 1; day <= 31; day++)
            {
                string key = $"{persianMonth}/{day}";
                if (HolidayEvents.ContainsKey(key))
                {
                    events.Add($"{day}: {HolidayEvents[key]}");
                }
            }
            
            return events.ToArray();
        }

        public string? GetTodayEvent(int persianMonth, int persianDay)
        {
            string key = $"{persianMonth}/{persianDay}";
            return HolidayEvents.ContainsKey(key) ? HolidayEvents[key] : null;
        }
    }
}
