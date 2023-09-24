using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using TaskManagementApp.ViewModels;

namespace TaskManagementApp.Services
{
    public class TaskManagerService
    {
        // Save tasks to a JSON file
        public void SaveTasksToJson(string filePath, List<TaskViewModel> tasks)
        {
            TaskData data = new TaskData { Tasks = tasks };
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // Load tasks from a JSON file with error handling
        public List<TaskViewModel> LoadTasksFromJson(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    TaskData data = JsonConvert.DeserializeObject<TaskData>(json);
                    return data.Tasks;
                }
                else
                {
                    return new List<TaskViewModel>();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"An IO error occurred while reading the file: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
