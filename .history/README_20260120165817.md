# 📌 Tổng quan về dự án .NET MVC

---

## 🗂️ Cấu trúc thư mục của dự án .NET MVC

Một dự án ASP.NET MVC tiêu chuẩn thường bao gồm các thành phần sau:

### 📁 Tên Project

* Là **tên ứng dụng** của bạn
* Thường trùng với namespace chính của project

### 📁 Controllers

* Chứa các **Controller**
* Controller là nơi **xử lý các request** được gửi từ View
* Mỗi controller là một file `.cs`

### 📁 Models

* Chứa các **lớp đại diện cho dữ liệu** của ứng dụng
* Thường ánh xạ với **bảng trong CSDL** (Entity)
* Dùng để trao đổi dữ liệu giữa Controller và View

### 📁 Views

* Chứa các file giao diện người dùng (`.cshtml`)
* Mỗi View tương ứng với **một Action trong Controller**
* Chịu trách nhiệm hiển thị dữ liệu

### 📁 wwwroot

* Chứa các **file tĩnh** của dự án như:

  * HTML
  * CSS
  * JavaScript
  * Images

### ⚙️ appsettings.json

* Chứa **cấu hình của hệ thống**
* Ví dụ:

  * Chuỗi kết nối CSDL (Connection String)
  * Cấu hình môi trường (Development / Production)

### ⚙️ Program.cs

* File **khởi động ứng dụng**
* Cấu hình:

  * Middleware
  * Routing
  * Dependency Injection

---

## 🔁 Định tuyến (Routing) trong .NET MVC

### 📌 Routing là gì?

Routing quyết định **URL sẽ gọi Controller và Action nào** trong ứng dụng MVC.

### 🧠 Nguyên lý hoạt động

ASP.NET MVC sử dụng logic:

```
Controller / Action / Parameter
```

Ví dụ:

```
https://localhost:5001/Student/Detail/5
```

* `Student` → Controller
* `Detail` → Action
* `5` → Parameter (id)

### ⚙️ Cấu hình Routing trong `Program.cs`

```csharp
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

🔹 Ý nghĩa:

* `controller=Home` → Controller mặc định là **HomeController**
* `action=Index` → Action mặc định là **Index()**
* `id?` → Tham số tùy chọn

---

## 🧩 Controller & View trong .NET MVC

### 🎮 Controller

* Nằm trong thư mục **Controllers**
* Tên controller **bắt buộc có hậu tố `Controller`**

📌 Ví dụ:

```csharp
StudentController
```

### ✨ Nhiệm vụ của Controller

* Nhận và xử lý request từ người dùng
* Truy xuất dữ liệu từ CSDL (thông qua Model)
* Gọi View và truyền dữ liệu sang View

### 🧱 Controller mặc định

Khi tạo project MVC mới, hệ thống sẽ tự động tạo:

```text
HomeController.cs
```

Trong đó thường có các Action:

* `Index()`
* `Privacy()`

Mỗi Action:

* Thực hiện **một chức năng cụ thể**
* Trả về **View tương ứng**

---

### 🖼️ View

* View là các file `.cshtml`
* Nằm trong thư mục **Views**
* Có nhiệm vụ **hiển thị dữ liệu cho người dùng**

📌 Mối quan hệ:

* Mỗi Action trong Controller thường có **một View cùng tên**

---

## ➕ Tạo Controller mới

Để tạo một Controller mới:

1. Chuột phải vào thư mục **Controllers**
2. Chọn **Add → Controller** hoặc **Add → New file**
3. Tạo file `.cs`
4. Đặt tên theo đúng quy tắc: `TênControllerController`

📌 Ví dụ:

```text
StudentController.cs
```

---

## ✅ Tổng kết

* MVC gồm **Model – View – Controller**
* Routing quyết định **URL gọi Controller/Action nào**
* Controller xử lý logic, View hiển thị giao diện
* `Program.cs` và `appsettings.json` là trung tâm cấu hình

---

✨ *Tài liệu này phù hợp để dùng làm README cho project ASP.NET MVC.*
