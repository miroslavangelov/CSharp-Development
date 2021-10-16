using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using Newtonsoft.Json;
using TeisterMask.Data.Models.Enums;
using TeisterMask.DataProcessor.ExportDto;

namespace TeisterMask.DataProcessor
{
    using System;

    using Data;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var result = context.Projects
                .Where(project => project.Tasks.Any())
                .Select(project => new ExportProjectDto
                {
                    TasksCount = project.Tasks.Count,
                    ProjectName = project.Name,
                    HasEndDate = project.DueDate.HasValue ? "Yes" : "No",
                    Tasks = project.Tasks
                        .Select(task => new ExportTaskDto
                        {
                            Name = task.Name,
                            Label = task.LabelType.ToString()
                        })
                        .OrderBy(task => task.Name)
                        .ToList()
                })
                .ToList()
                .OrderByDescending(project => project.Tasks.Count)
                .ThenBy(project => project.ProjectName)
                .ToList();

            return XMLConverter.Serialize(result, "Projects");
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var result = context.Employees
                .Where(employee => employee.EmployeesTasks.Any(employeeTask => employeeTask.Task.OpenDate >= date))
                .Select(employee => new
                {
                    Username = employee.Username,
                    Tasks = employee.EmployeesTasks
                        .Where(employeeTask => employeeTask.Task.OpenDate > date)
                        .OrderByDescending(employeeTask => employeeTask.Task.DueDate)
                        .ThenBy(employeeTask => employeeTask.Task.Name)
                        .Select(employeeTask => new
                        {
                            TaskName = employeeTask.Task.Name,
                            OpenDate = employeeTask.Task.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = employeeTask.Task.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = employeeTask.Task.LabelType.ToString(),
                            ExecutionType = employeeTask.Task.ExecutionType.ToString()
                        })
                        .ToList()
                })
                .ToList()
                .OrderByDescending(employee => employee.Tasks.Count)
                .ThenBy(employee => employee.Username)
                .Take(10)
                .ToList();

            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }
    }
}