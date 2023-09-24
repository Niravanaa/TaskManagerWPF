using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaskManagementApp.ViewModels;

[Serializable]
public class TaskData
{
    [JsonProperty("tasks")] // Specify the JSON property name
    public List<TaskViewModel> Tasks { get; set; }

    public TaskData()
    {
        Tasks = new List<TaskViewModel>();
    }
}
