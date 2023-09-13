using TaskManagementApp.Models;

namespace TaskManagementApp.ViewModels
{
    public class TaskViewModel
    {
        public Task TaskModel { get; }

        public TaskViewModel(Task task)
        {
            TaskModel = task;
        }
        public string Title
        {
            get => TaskModel.Title;
            set => TaskModel.Title = value;
        }
    }
}
