# Task Management API

## 1. Giới thiệu

Đây là RESTful API dùng để quản lý công việc, bao gồm:

* User (người dùng)
* Project (dự án)
* Task (công việc)

API hỗ trợ phân công task (assign), cập nhật trạng thái và xác thực bằng JWT.

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

git clone <link-repo-của-bạn>

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

dotnet run

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

Bearer {your_token}

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

Sử dụng Swagger để test theo flow:

1. Register
2. Login
3. Authorize
4. Create Project
5. Create Task
6. Assign Task
7. Update Status
8. Get Tasks

---

## 8. Tài khoản test

Email: [test@gmail.com](mailto:test@gmail.com)
Password: 123456

---

## 9. Ghi chú

* Sử dụng SQL Server local
* Cần chạy migration trước khi run
* Đảm bảo SQL Server đang hoạt động
