## User 
- Id: Guid
- Name: string


## Task
- Id: Guid
- Title: string
- Status: TaskStatus (TODO, IN_PROGRESS, DONE)
- AssignedUserId: User

## Project
- Id: Guid
- Name: string
- Tasks: List<Task>