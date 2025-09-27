# Project API - Optimized Version

این پروژه یک API بهینه‌شده برای مدیریت اطلاعات افراد، شهرها و استان‌ها است.

## ویژگی‌های بهینه‌سازی

### 🚀 بهبودهای عملکرد
- **Repository Pattern**: جداسازی منطق دسترسی به داده
- **DTO Pattern**: جداسازی مدل‌های Entity از API
- **Pagination**: پشتیبانی از صفحه‌بندی برای لیست‌ها
- **Caching**: استفاده از Memory Cache برای بهبود عملکرد
- **Query Optimization**: بهینه‌سازی کوئری‌های دیتابیس

### 🛡️ امنیت و قابلیت اطمینان
- **Global Exception Handling**: مدیریت خطاهای سراسری
- **Logging**: استفاده از Serilog برای لاگ‌گیری
- **Health Checks**: بررسی وضعیت سیستم
- **Input Validation**: اعتبارسنجی ورودی‌ها

### 📊 نظارت و مانیتورینگ
- **Structured Logging**: لاگ‌های ساختاریافته
- **Performance Monitoring**: نظارت بر عملکرد
- **Error Tracking**: ردیابی خطاها

## ساختار پروژه

```
Project/
├── Controllers/          # کنترلرهای API
├── Data/                # Context دیتابیس
├── DTOs/                # Data Transfer Objects
├── Middleware/          # Middleware های سفارشی
├── Models/              # مدل‌های Entity
├── Repositories/        # Repository Pattern
├── Services/            # سرویس‌های کسب‌وکار
└── PerformanceTests/    # تست‌های عملکرد
```

## API Endpoints

### Persons
- `GET /api/persons` - دریافت لیست افراد (با pagination)
- `GET /api/persons/{id}` - دریافت فرد خاص
- `POST /api/persons` - ایجاد فرد جدید
- `PUT /api/persons/{id}` - به‌روزرسانی فرد
- `DELETE /api/persons/{id}` - حذف فرد

### Health Check
- `GET /health` - بررسی وضعیت سیستم

## نحوه اجرا

1. **نصب وابستگی‌ها**:
   ```bash
   dotnet restore
   ```

2. **تنظیم Connection String** در `appsettings.json`

3. **اجرای Migration**:
   ```bash
   dotnet ef database update
   ```

4. **اجرای پروژه**:
   ```bash
   dotnet run
   ```

## بهبودهای اعمال شده

### قبل از بهینه‌سازی:
- ❌ N+1 Query Problem
- ❌ عدم وجود Repository Pattern
- ❌ عدم وجود Caching
- ❌ عدم وجود Pagination
- ❌ عدم وجود Error Handling
- ❌ عدم وجود Logging

### بعد از بهینه‌سازی:
- ✅ Repository Pattern برای جداسازی منطق
- ✅ DTO Pattern برای جداسازی مدل‌ها
- ✅ Memory Caching برای بهبود عملکرد
- ✅ Pagination برای مدیریت داده‌های بزرگ
- ✅ Global Exception Handling
- ✅ Structured Logging با Serilog
- ✅ Health Checks
- ✅ بهینه‌سازی کوئری‌های دیتابیس

## Performance Improvements

- **کاهش N+1 Queries**: استفاده از Include بهینه
- **Caching**: کاهش درخواست‌های دیتابیس
- **Pagination**: کاهش حجم داده‌های انتقالی
- **Query Optimization**: استفاده از Select برای کاهش داده‌های انتقالی

## Dependencies

- .NET 8.0
- Entity Framework Core 8.0
- Serilog
- Swagger/OpenAPI
- Memory Caching
