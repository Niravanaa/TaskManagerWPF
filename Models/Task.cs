using System;

namespace TaskManagementApp.Models
{
    public class Task
    {
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
        public Priority SelectedPriority { get; set; }
        public DateTime? DueDate { get; set; } // Add the DueDate field
        public string Description { get; internal set; }

        public enum Priority
        {
            Low,
            Medium,
            High
        }
    }
}
