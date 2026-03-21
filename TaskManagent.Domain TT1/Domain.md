# Task Management Domain 
## Mô tả hệ thống
Hệ thống quản lý các task trong một project 
Mỗi task có thể gán cho một user và có trạng thái.


## Các thực thể chính
- User: Người sử dụng hệ thống
- Task: Công việc cần thực hiện
- Project: Dự án chứa các task

## Nghiệp vụ chính
- User có thể tạo các task
- Task khi tạo mặc định là TODO
- Task có thể gán cho user
- Task có luồng trạng thái: TODO -> IN_PROGRESS -> DONE