- Đã tạo index cho Status
- Tránh SELECT *
- Dùng WHERE rõ ràng


CREATE DATABASE TaskManagement_TT1;
GO

USE TaskManagement_TT1;
GO


CREATE TABLE Users (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Projects (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL
);

CREATE TABLE Tasks (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Status INT NOT NULL,
    AssignedUserId UNIQUEIDENTIFIER NULL,
    ProjectId UNIQUEIDENTIFIER NOT NULL
);

ALTER TABLE Tasks
ADD CONSTRAINT FK_Tasks_Users
FOREIGN KEY (AssignedUserId) REFERENCES Users(Id);

ALTER TABLE Tasks
ADD CONSTRAINT FK_Tasks_Projects
FOREIGN KEY (ProjectId) REFERENCES Projects(Id);

CREATE INDEX IX_Tasks_Status ON Tasks(Status);
CREATE INDEX IX_Tasks_AssignedUserId ON Tasks(AssignedUserId);

INSERT INTO Users VALUES (NEWID(), N'Phát');
INSERT INTO Projects VALUES (NEWID(), N'Dự án học tập');
-- thêm nhiều record

ALTER TABLE Tasks
ADD CONSTRAINT CK_Tasks_Status
CHECK (Status IN (0, 1, 2));



SELECT * FROM Tasks
WHERE AssignedUserId = '...';

SELECT * FROM Tasks
WHERE Status = 1;

SELECT * FROM Tasks
WHERE ProjectId = '...';
SELECT * FROM Tasks;
SELECT * FROM Projects;
SELECT * FROM Users;

