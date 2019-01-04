using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace اذان_و_اوقات_شرعی.Medol
{
    public static class DateTimeHelper
    {
        //public static NumberDayOfWeek NumberDayOfWeeks(this DateTime date)
        //{
        //    switch (date.DayOfWeek)
        //    {
        //        case DayOfWeek.Saturday:
        //            return NumberDayOfWeek.Shanbe;
        //        case DayOfWeek.Sunday:
        //            return NumberDayOfWeek.Yekshanbe;
        //        case DayOfWeek.Monday:
        //            return NumberDayOfWeek.Doshanbe;
        //        case DayOfWeek.Tuesday:
        //            return NumberDayOfWeek.Seshanbe;
        //        case DayOfWeek.Wednesday:
        //            return NumberDayOfWeek.Charshanbe;
        //        case DayOfWeek.Thursday:
        //            return  NumberDayOfWeek.Panjshanbe;
        //        case DayOfWeek.Friday:
        //            return NumberDayOfWeek.Jome;
        //        default:
        //            throw new Exception();
        //    }
        //}
        public static string PersionDayOfWeek(this DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return "شنبه";
                case DayOfWeek.Sunday:
                    return"یک شنبه";
                case DayOfWeek.Monday:
                    return "دو شنبه";
                case DayOfWeek.Tuesday:
                    return "سه شنبه";
                case DayOfWeek.Wednesday:
                    return "چهار شنبه";
                case DayOfWeek.Thursday:
                    return "پنج شنبه";
                case DayOfWeek.Friday:
                    return "جمعه";
                default:
                    throw new Exception();
            }
        }
        //public enum NumberDayOfWeek
        //{
        //    Shanbe = 1,
        //    Yekshanbe = 2,
        //    Doshanbe = 3,
        //    Seshanbe = 4,
        //    Charshanbe = 5,
        //    Panjshanbe = 6,
        //    Jome = 7
        //}
    }
}
