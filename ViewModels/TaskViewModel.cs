using System;
using System.ComponentModel;
using TaskManagementApp.Models;
using static TaskManagementApp.Models.Task;

namespace TaskManagementApp.ViewModels
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TaskViewModel : ObservableObject
    {
        private Task _taskModel;

        public TaskViewModel()
        {
            _taskModel = new Task();
        }

        public TaskViewModel(Task task)
        {
            _taskModel = task;
        }

        public string Title
        {
            get => _taskModel.Title;
            set
            {
                _taskModel.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public Task TaskModel
        {
            get => _taskModel;
            set
            {
                _taskModel = value;
                OnPropertyChanged(nameof(TaskModel));
            }
        }

        public string Description
        {
            get => _taskModel.Description;
            set
            {
                _taskModel.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime? DueDate
        {
            get => _taskModel.DueDate;
            set
            {
                _taskModel.DueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public Priority SelectedPriority
        {
            get => _taskModel.SelectedPriority;
            set
            {
                _taskModel.SelectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
            }
        }
    }
}
