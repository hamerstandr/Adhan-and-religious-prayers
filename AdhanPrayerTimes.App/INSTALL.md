# راهنمای نصب و راه‌اندازی

## پیش‌نیازها

### 1. نصب .NET 10 SDK

#### روش اول: دانلود از سایت مایکروسافت
1. به آدرس https://dotnet.microsoft.com/download بروید
2. نسخه .NET 10 SDK را دانلود کنید
3. فایل نصبی را اجرا کرده و مراحل را دنبال کنید

#### روش دوم: استفاده از PowerShell (ویندوز)
```powershell
winget install Microsoft.DotNet.SDK.10
```

#### روش سوم: استفاده از خط فرمان (لینوکس)
```bash
# Ubuntu/Debian
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update && sudo apt-get install -y dotnet-sdk-10.0

# Fedora/RHEL
sudo dnf install -y dotnet-sdk-10.0
```

### 2. بررسی نصب
پس از نصب، دستور زیر را در ترمینال اجرا کنید:
```bash
dotnet --version
```
باید نسخه 10.0.x را نمایش دهد.

## نصب پروژه

### مرحله 1: کپی فایل‌ها
تمام فایل‌های پوشه `AdhanPrayerTimes.App` را به یک پوشه دلخواه کپی کنید.

### مرحله 2: بازیابی پکیج‌ها
```bash
cd AdhanPrayerTimes.App
dotnet restore
```

### مرحله 3: کامپایل پروژه
```bash
dotnet build
```

### مرحله 4: اجرای برنامه
```bash
dotnet run
```

## ساخت فایل اجرایی مستقل

برای ساخت فایلی که بدون نیاز به نصب .NET اجرا شود:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

خروجی در مسیر زیر قرار می‌گیرد:
```
bin/Release/net10.0-windows/win-x64/publish/AdhanPrayerTimes.exe
```

## عیب‌یابی

### خطای "dotnet is not recognized"
- مطمئن شوید .NET SDK نصب شده است
- سیستم را ری‌استارت کنید
- متغیر محیطی PATH را بررسی کنید

### خطای "NU1101: Unable to find package"
```bash
dotnet nuget locals all --clear
dotnet restore --force
```

### خطای مربوط به WPF در لینوکس/مک
این برنامه فقط روی ویندوز اجرا می‌شود زیرا از WPF استفاده می‌کند.

## به‌روزرسانی

برای دریافت آخرین تغییرات:
```bash
git pull origin main
dotnet restore
dotnet build
```

## حذف نصب

فقط کافیست پوشه برنامه را حذف کنید. هیچ فایل دیگری در سیستم ثبت نمی‌شود.

---

**پشتیبانی**: برای مشکلات بیشتر، مستندات فنی را مطالعه کنید.
