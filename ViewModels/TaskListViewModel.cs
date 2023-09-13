using System.Collections.ObjectModel;

namespace TaskManagementApp.ViewModels
{
    public class TaskListViewModel
    {
        public ObservableCollection<TaskViewModel> Tasks { get; set; }

        public TaskListViewModel()
        {
            // Initialize and populate Tasks collection with sample tasks here
        }
    }
}