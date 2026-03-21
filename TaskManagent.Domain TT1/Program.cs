using TaskManagent.Domain_TT1.Entities;


var user = new User("Tấn Phát");
var project = new Project("Dự án quản lý công việc");

var task1 = new TaskItem("Learn C# OOP");
var task2 = new TaskItem("Practice domain design");

project.AddTask(task1);
project.AddTask(task2);

task1.AssignUser(user);
task1.MoveToInProgress();

Console.WriteLine($"Project: {project.Name}");
foreach (var task in project.Tasks)
{
    Console.WriteLine($" - {task.Title} | Status: {task.Status}");
}