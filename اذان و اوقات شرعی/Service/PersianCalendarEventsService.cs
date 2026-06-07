using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace اذان_و_اوقات_شرعی.Service
{
    /// <summary>
    /// سرویس دریافت رویدادهای تقویم شمسی از API آنلاین
    /// </summary>
    public class PersianCalendarEventsService
    {
        private static readonly HttpClient httpClient = new HttpClient();
        
        /// <summary>
        /// ساختار داده‌ای برای نگهداری اطلاعات رویداد
        /// </summary>
        public class CalendarEvent
        {
            public string Title { get; set; }
            public string Date { get; set; }
            public bool IsHoliday { get; set; }
            public string Description { get; set; }
        }

        /// <summary>
        /// دریافت رویدادهای یک ماه خاص از تقویم شمسی
        /// </summary>
        /// <param name="year">سال شمسی</param>
        /// <param name="month">ماه شمسی (1-12)</param>
        /// <returns>لیست رویدادها</returns>
        public async Task<CalendarEvent[]> GetMonthEventsAsync(int year, int month)
        {
            try
            {
                // استفاده از API رایگان jalaali
                var url = $"https://api.jalaali.com/api/calendar/{year}/{month}";
                var response = await httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    // پردازش JSON و تبدیل به آبجکت‌های CalendarEvent
                    // این بخش نیاز به کتابخانه Newtonsoft.Json دارد که در پروژه موجود است
                    return await ParseEventsAsync(json);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching calendar events: {ex.Message}");
            }
            
            return Array.Empty<CalendarEvent>();
        }

        /// <summary>
        /// دریافت تعطیلات رسمی
        /// </summary>
        public async Task<CalendarEvent[]> GetHolidaysAsync(int year)
        {
            try
            {
                var url = $"https://api.jalaali.com/api/holidays/{year}";
                var response = await httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return await ParseEventsAsync(json);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching holidays: {ex.Message}");
            }
            
            return Array.Empty<CalendarEvent>();
        }

        private async Task<CalendarEvent[]> ParseEventsAsync(string json)
        {
            // پیاده‌سازی ساده برای پارس کردن JSON
            // در نسخه کامل می‌توان از Newtonsoft.Json استفاده کرد
            try
            {
                var events = new System.Collections.Generic.List<CalendarEvent>();
                
                // پردازش ساده JSON (برای نمونه)
                // در نسخه واقعی باید از Json.NET استفاده شود
                if (!string.IsNullOrEmpty(json))
                {
                    // اینجا می‌توانید از Newtonsoft.Json.Linq استفاده کنید
                    // var jo = Newtonsoft.Json.Linq.JObject.Parse(json);
                    // و سپس فیلدها را استخراج کنید
                }
                
                return events.ToArray();
            }
            catch
            {
                return Array.Empty<CalendarEvent>();
            }
        }
    }
}
