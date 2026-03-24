# Task Management API

## 1. Giới thiệu

Đây là RESTful API dùng để quản lý công việc, bao gồm:

* User (người dùng)
* Project (dự án)
* Task (công việc)

API hỗ trợ phân công task (assign), cập nhật trạng thái và xác thực bằng JWT.

## 1.1 Slide cấu trúc

Slide PDF: WebAPI_TT1.1.Bản thuyết trình Thực tập kỳ spring 2026.pdf
Link slide cấu trúc: [https://www.canva.com/design/DAHEu9wgjE0/Yipd8YrWKCqIcL9LyKVp2g/edit?utm_content=DAHEu9wgjE0&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton](https://www.canva.com/design/DAHEu9wgjE0/Yipd8YrWKCqIcL9LyKVp2g/edit?utm_content=DAHEu9wgjE0&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton)

EF Core là ORM giúp tương tác database bằng LINQ thay vì SQL,
giúp code dễ đọc và bảo trì.
---


## 2. Công nghệ sử dụng

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger (API testing)
* xUnit + Moq (Unit Test)

---

## 3. Hướng dẫn cài đặt

### Clone project

git clone <https://github.com/TanPhattttt/ThucTapSpring2026_TaskManagement>

---

### Cấu hình database

Mở file `appsettings.json` và sửa:

"ConnectionStrings": {
"DefaultConnection": "Server=.;Database=TaskDB_Dev;Trusted_Connection=True;TrustServerCertificate=True"
}

---

### Chạy migration

update-database

---

### Run project

Hoặc chạy bằng Visual Studio (F5)

---

## 4. Sử dụng API (Swagger)

Sau khi chạy project, mở:

[https://localhost:xxxx/swagger](https://localhost:xxxx/swagger)

---

## 5. Authentication (JWT)

### Bước 1: Register

POST /api/auth/register

### Bước 2: Login

POST /api/auth/login

→ Nhận token

---

### Bước 3: Authorize

Nhấn nút **Authorize** trên Swagger và nhập:

{your_token}

---

## 6. Các API chính

### Auth

* POST /api/auth/register
* POST /api/auth/login

### Project

* POST /api/project

### Task

* POST /api/task
* PUT /api/task/assign
* PUT /api/task/update-status
* GET /api/task

---

## 7. Test API

Thực hiện theo các bước sau để test API:

1. Đăng ký user
   POST /api/auth/register

2. Đăng nhập
   POST /api/auth/login
   → Copy token trả về

3. Authorize
   → Nhấn "Authorize" trong Swagger
   → Nhập: Bearer {your_token}

4. Tạo Project
   POST /api/project

5. Tạo Task
   POST /api/task

6. Assign Task cho user
   PUT /api/task/assign

7. Update trạng thái Task
   PUT /api/task/update-status

8. Lấy danh sách Task
   GET /api/task

---

## 8. Tài khoản test

Email: [admin@gmail.com]
Password: admin
(Create Admin user bằng cách vào AuthController, đổi mã RoleId 11111111-1111-1111-1111-111111111111 thành 22222222-2222-2222-2222-222222222222)

---

## 9. Ghi chú

* Sử dụng SQL Server local
* Cần chạy migration trước khi run
* Đảm bảo SQL Server đang hoạt động

---

## 10. Task Assign
Chức năng Assign dùng để:

- Phân công task cho user
- Xác định người chịu trách nhiệm
- Hỗ trợ quản lý công việc theo từng cá nhân

---

## 11. What I Learned

* Xây dựng RESTful API với ASP.NET Core
* Sử dụng JWT Authentication
* Làm việc với Entity Framework Core
* Tổ chức code theo mô hình 3-layer
* Viết unit test với xUnit + Moq


