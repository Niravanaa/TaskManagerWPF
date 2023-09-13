using System.Collections.ObjectModel;
using System.Windows;
using TaskManagementApp.ViewModels;
using TaskManagementApp.Models;

namespace TaskManagementApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskViewModel> tasks;

        public MainWindow()
        {
            InitializeComponent();
            tasks = new ObservableCollection<TaskViewModel>();
            TaskListBox.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            // Add a new task to the tasks collection
            string taskTitle = TaskInputBox.Text;
            if (!string.IsNullOrWhiteSpace(taskTitle))
            {
                TaskViewModel newTask = new TaskViewModel(new Task { Title = taskTitle });
                tasks.Add(newTask);
                TaskInputBox.Clear();

                // Refresh the ListBox
                TaskListBox.ItemsSource = tasks;
            }
        }


        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            // Edit the selected task
            TaskViewModel? selectedTask = TaskListBox.SelectedItem as TaskViewModel;
            if (selectedTask != null)
            {
                string newTitle = TaskInputBox.Text;
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    selectedTask.TaskModel.Title = newTitle;
                    TaskInputBox.Clear();
                }
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Delete the selected task
            TaskViewModel? selectedTask = TaskListBox.SelectedItem as TaskViewModel;
            if (selectedTask != null)
            {
                tasks.Remove(selectedTask);
                TaskInputBox.Clear();
            }
        }
    }
}
