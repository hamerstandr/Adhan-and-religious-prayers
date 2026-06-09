# مستندات فنی پروژه اذان و اوقات شرعی

## فهرست مطالب
1. [معماری نرم‌افزار](#معماری-نرم‌افزار)
2. [کلاس‌ها و اجزا](#کلاس‌ها-و-اجزا)
3. [الگوریتم‌ها](#الگوریتم‌ها)
4. [جریان داده](#جریان-داده)
5. [توسعه و نگهداری](#توسعه-و-نگهداری)

---

## معماری نرم‌افزار

### نمای کلی
این پروژه از الگوی **MVVM (Model-View-ViewModel)** پیروی می‌کند که باعث جداسازی منطق کسب‌وکار از رابط کاربری می‌شود.

```
┌─────────────────────────────────────────────────────┐
│                    View (XAML)                       │
│  - MainWindow.xaml                                   │
│  - App.xaml                                          │
│  - UI Elements, Styles, Templates                   │
└─────────────────────────────────────────────────────┘
                          ↕ Data Binding
┌─────────────────────────────────────────────────────┐
│              ViewModel (PrayerTimeViewModel)         │
│  - INotifyPropertyChanged Implementation            │
│  - Data Properties                                  │
│  - Commands                                         │
└─────────────────────────────────────────────────────┘
                          ↕ Calls
┌─────────────────────────────────────────────────────┐
│                  Services Layer                      │
│  - PrayerTimeService                                 │
│  - CityService                                       │
│  - PersianCalendarEventsService                     │
└─────────────────────────────────────────────────────┘
```

### لایه‌ها

#### 1. Presentation Layer (لایه نمایش)
- **MainWindow.xaml**: تعریف UI با XAML
- **App.xaml**: منابع و استایل‌های سراسری

#### 2. ViewModel Layer (لایه مدل-نما)
- **PrayerTimeViewModel.cs**: پل بین View و Model
- پیاده‌سازی `INotifyPropertyChanged` برای به‌روزرسانی خودکار UI

#### 3. Service Layer (لایه سرویس)
- **PrayerTimeService**: محاسبات نجومی
- **CityService**: مدیریت داده‌های شهرها
- **PersianCalendarEventsService**: رویدادهای تقویمی

#### 4. Model Layer (لایه مدل)
- **City**: مدل داده شهر
- ساختارهای داده ساده

---

## کلاس‌ها و اجزا

### PrayerTimeViewModel

**مسیر**: `Models/PrayerTimeViewModel.cs`

**وظیفه**: نگهداری داده‌های نمایشی و اطلاع‌رسانی تغییرات به View

**Properties**:
| نام | نوع | توضیحات |
|-----|-----|---------|
| Fajr | string | زمان اذان صبح |
| Sunrise | string | زمان طلوع آفتاب |
| Dhuhr | string | زمان اذان ظهر |
| Sunset | string | زمان غروب آفتاب |
| Maghrib | string | زمان اذان مغرب |
| Midnight | string | زمان نیمه شب شرعی |
| PersianDate | string | تاریخ شمسی |
| GregorianDate | string | تاریخ میلادی |
| LunarDate | string | تاریخ قمری |
| Events | string | رویدادهای روز |
| SelectedCity | string | شهر انتخاب شده |

**متدها**:
```csharp
protected virtual void OnPropertyChanged(string? propertyName = null)
```
اطلاع‌رسانی تغییر property به View

---

### PrayerTimeService

**مسیر**: `Services/PrayerTimeService.cs`

**وظیفه**: محاسبه اوقات شرعی بر اساس موقعیت جغرافیایی و تاریخ

**متدهای عمومی**:
```csharp
void SetLocation(double longitude, double latitude, int month, int day)
string GetFajr()
string GetSunrise()
string GetDhuhr()
string GetSunset()
string GetMaghrib()
string GetMidnight()
```

**متدهای خصوصی**:
```csharp
string CalculatePrayerTime(PrayerType type)
string FormatTime(double decimalTime)
```

**الگوریتم محاسبه**:
1. محاسبه روز سال
2. محاسبه معادله زمان
3. محاسبه انحراف خورشیدی
4. محاسبه زاویه ساعتی
5. اعمال اصلاحات برای هر نماز

---

### CityService

**مسیر**: `Services/CityService.cs`

**وظیفه**: ارائه لیست شهرهای ایران با مختصات جغرافیایی

**متد**:
```csharp
List<City> GetCities()
```

**خروجی**: لیستی از ۳۰ شهر بزرگ ایران

---

### PersianCalendarEventsService

**مسیر**: `Services/PersianCalendarEventsService.cs`

**وظیفه**: مدیریت رویدادهای تقویم فارسی

**متدها**:
```csharp
string[] GetMonthEvents(int persianYear, int persianMonth)
string? GetTodayEvent(int persianMonth, int persianDay)
```

**داده‌ها**: دیکشنری از مناسبت‌های رسمی و مذهبی

---

### MainWindow

**مسیر**: `Views/MainWindow.xaml.cs`

**وظیفه**: مدیریت تعاملات کاربر و هماهنگی بین کامپوننت‌ها

**فیلدهای اصلی**:
```csharp
PrayerTimeViewModel _viewModel
PrayerTimeService _prayerService
CityService _cityService
PersianCalendarEventsService _calendarService
DispatcherTimer _timer
System.Media.SoundPlayer? _soundPlayer
```

**متدهای مهم**:
```csharp
void Timer_Tick(object? sender, EventArgs e)
void PlayAzan()
void UpdateClockDisplay()
void UpdatePrayerTimes()
string GetLunarDate(DateTime date)
```

---

## الگوریتم‌ها

### 1. محاسبه اوقات شرعی

#### ورودی‌ها:
- طول جغرافیایی (Longitude)
- عرض جغرافیایی (Latitude)
- ماه و روز

#### مراحل محاسبه:

**گام 1: محاسبه روز سال**
```csharp
var dayOfYear = date.DayOfYear;
```

**گام 2: زمان پایه ظهر شرعی**
```csharp
double baseTime = 12.0 + (longitude / 15.0);
```

**گام 3: معادله زمان (Equation of Time)**
```csharp
double equationOfTime = -7.6 * Math.Sin(0.0327 * dayOfYear) 
                       - 9.9 * Math.Cos(0.0327 * dayOfYear);
```

**گام 4: انحراف خورشیدی (Declination)**
```csharp
double declination = 23.45 * Math.Sin(0.0172 * (dayOfYear - 80));
```

**گام 5: زاویه ساعتی (Hour Angle)**
```csharp
double hourAngle = Math.Acos((Math.Cos(Math.PI * 90.833 / 180) - 
    Math.Sin(Math.PI * latitude / 180) * Math.Sin(Math.PI * declination / 180)) /
    (Math.Cos(Math.PI * latitude / 180) * Math.Cos(Math.PI * declination / 180)));
```

**گام 6: محاسبه زمان هر نماز**
```csharp
Fajr:     baseTime - sunriseOffset - 1.5 + equationOfTime / 60.0
Sunrise:  baseTime - sunriseOffset + equationOfTime / 60.0
Dhuhr:    baseTime + equationOfTime / 60.0
Sunset:   baseTime + sunriseOffset + equationOfTime / 60.0
Maghrib:  baseTime + sunriseOffset + 0.3 + equationOfTime / 60.0
Midnight: baseTime + 12.0 + equationOfTime / 60.0
```

---

### 2. محاسبه تاریخ قمری

#### الگوریتم Meeus's Lunar Algorithm (ساده‌شده)

**گام 1: تبدیل به روز جولیان**
```csharp
if (jm > 6) jm -= 6;
else { jy -= 1; jm += 6; }

int jDay = jd + ((jm - 1) * 30) + (jy * 365) + 
           ((jy / 4) - (jy / 100) + (jy / 400)) + 1948320;
```

**گام 2: محاسبه سن ماه**
```csharp
double lunarAge = ((jDay - 1948084) % 29.530588);
if (lunarAge < 0) lunarAge += 29.530588;
```

**گام 3: تعیین ماه و روز قمری**
```csharp
int lunarMonth = (int)((lunarAge / 29.530588) * 12) + 1;
if (lunarMonth > 12) lunarMonth = 1;

int lunarDay = (int)lunarAge + 1;
if (lunarDay > 30) lunarDay = 1;
```

**گام 4: فرمت‌بندی خروجی**
```csharp
return $"{lunarDay} {lunarMonthNames[lunarMonth - 1]} {lunarYear}";
```

---

## جریان داده

### 1. راه‌اندازی برنامه

```
App.OnStartup()
    ↓
MainWindow Constructor
    ↓
Initialize Services
    ↓
Load Cities → ComboBox
    ↓
Start Timer
    ↓
UpdatePrayerTimes()
    ↓
Update UI via Data Binding
```

### 2. تغییر شهر

```
User selects city
    ↓
ComboBoxCity_SelectionChanged
    ↓
UpdatePrayerTimes()
    ↓
PrayerTimeService.SetLocation()
    ↓
Calculate all prayer times
    ↓
Update ViewModel properties
    ↓
INotifyPropertyChanged notifies UI
    ↓
UI updates automatically
```

### 3. تایمر و اذان

```
Timer.Tick (every second)
    ↓
Check current time vs prayer times
    ↓
If match found:
    - PlayAzan()
    - Show MessageBox
    ↓
Every minute (seconds == 0):
    - UpdateClockDisplay()
    - Update dates
    - Check for today's events
```

---

## توسعه و نگهداری

### افزودن ویژگی جدید

#### مثال: اضافه کردن آلارم سفارشی

**گام 1: افزودن Property به ViewModel**
```csharp
public bool IsCustomAlarmEnabled { get; set; }
public TimeSpan CustomAlarmTime { get; set; }
```

**گام 2: افزودن UI در XAML**
```xml
<CheckBox Content="فعال‌سازی آلارم سفارشی" 
          IsChecked="{Binding IsCustomAlarmEnabled}"/>
<TextBox Text="{Binding CustomAlarmTime}"/>
```

**گام 3: به‌روزرسانی منطق در MainWindow**
```csharp
private void Timer_Tick(object? sender, EventArgs e)
{
    // ... existing code ...
    
    if (_viewModel.IsCustomAlarmEnabled && 
        currentTime == _viewModel.CustomAlarmTime)
    {
        PlayCustomAlarm();
    }
}
```

### دیباگ و عیب‌یابی

#### فعال‌سازی لاگ

```csharp
// در MainWindow constructor
System.Diagnostics.Debug.WriteLine("App started");

// در متدهای مهم
System.Diagnostics.Debug.WriteLine($"Prayer times updated for {city.Name}");
```

#### استفاده از Debug Breakpoints
1. در Visual Studio روی خط مورد نظر کلیک کنید
2. کلید F9 را بزنید
3. برنامه را با F5 اجرا کنید

### تست واحد

برای افزودن تست واحد، پروژه تست ایجاد کنید:

```bash
dotnet new xunit -n AdhanPrayerTimes.Tests
```

مثال تست:

```csharp
[Fact]
public void GetFajr_ShouldReturnValidTimeFormat()
{
    var service = new PrayerTimeService();
    service.SetLocation(51.41, 35.69, 1, 1);
    
    var result = service.GetFajr();
    
    Assert.Matches(@"^\d{2}:\d{2}$", result);
}
```

---

## بهینه‌سازی

### بهبود عملکرد

1. **کاهش به‌روزرسانی‌های UI**:
   - فقط زمانی که داده تغییر می‌کند UI را به‌روز کنید
   - استفاده از `OnPropertyChanged` با نام property مشخص

2. **کش کردن محاسبات**:
```csharp
private Dictionary<string, string> _prayerTimeCache;

public string GetFajr()
{
    string key = $"{_latitude}_{_longitude}_{_month}_{_day}";
    if (_prayerTimeCache.ContainsKey(key))
        return _prayerTimeCache[key];
    
    var result = CalculatePrayerTime(PrayerType.Fajr);
    _prayerTimeCache[key] = result;
    return result;
}
```

3. **بارگذاری ناهمزمان**:
```csharp
private async Task LoadCalendarDataAsync()
{
    var events = await Task.Run(() => 
        _calendarService.GetMonthEvents(year, month));
    
    Application.Current.Dispatcher.Invoke(() =>
    {
        _viewModel.Events = string.Join(", ", events);
    });
}
```

---

## امنیت

### ملاحظات امنیتی

1. **ذخیره تنظیمات**: از `ConfigurationManager` یا `JSON` با رمزنگاری استفاده کنید
2. **دسترسی فایل**: محدود کردن دسترسی به فایل‌های سیستمی
3. **اعتبارسنجی ورودی**: بررسی مقادیر ورودی از کاربر

---

## نسخه‌بندی

### چangelog

**نسخه 2.0.0** (بازطراحی کامل)
- مهاجرت به .NET 10
- پیاده‌سازی MVVM
- UI مدرن با Material Design
- حذف وابستگی‌های سنگین

**نسخه 1.0.0** (نسخه اولیه)
- نمایش اوقات شرعی
- پشتیبانی از شهرهای ایران
- اعلان اذان

---

## منابع و مراجع

1. [الگوریتم محاسبه اوقات شرعی](https://www.alhabib.net/prayer-times-calculation/)
2. [تقویم فارسی](https://en.wikipedia.org/wiki/Solar_Hijri_calendar)
3. [الگوریتم قمری Meeus](https://en.wikipedia.org/wiki/Lunar_phase)
4. [الگوی MVVM مایکروسافت](https://docs.microsoft.com/en-us/dotnet/architecture/wpf/mvvm)

---

**تهیه شده برای پروژه اذان و اوقات شرعی**
**تاریخ آخرین به‌روزرسانی: 1403**
