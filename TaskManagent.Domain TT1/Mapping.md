## User → Users
- Id (Guid) → PK
- Name (nvarchar)

## Project → Projects
- Id (Guid) → PK
- Name (nvarchar)

## TaskItem → Tasks
- Id (Guid) → PK
- Title (nvarchar)
- Status (int)
- AssignedUserId (Guid, nullable)
- ProjectId (Guid)
