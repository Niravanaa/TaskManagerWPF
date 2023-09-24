using System;
using System.Collections.ObjectModel;
using System.Windows;
using TaskManagementApp.ViewModels;
using TaskManagementApp.Models;
using TaskManagementApp.Services;
using static TaskManagementApp.Models.Task;
using System.Collections.Generic;
using System.Windows.Controls;

namespace TaskManagementApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskViewModel> tasks;
        private TaskManagerService taskManagerService = new TaskManagerService();

        public MainWindow()
        {
            InitializeComponent();
            InitializeTasks();
        }

        private void InitializeTasks()
        {
            tasks = new ObservableCollection<TaskViewModel>();
            TaskListBox.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddNewTask();
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            EditSelectedTask();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedTask();
        }

        private void LoadTasks_Click(object sender, RoutedEventArgs e)
        {
            LoadTasksFromJson();
        }

        private void SaveTasks_Click(object sender, RoutedEventArgs e)
        {
            SaveTasksToJson();
        }

        private void PriorityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PriorityComboBox.SelectedIndex == 0) // Index of "Select Priority" item
            {
                // Don't allow selecting the placeholder text
                PriorityComboBox.SelectedIndex = -1; // Deselect it
            }
        }


        private void AddNewTask()
        {
            string taskTitle = TaskTitleTextBox.Text;
            string taskDescription = TaskDescriptionTextBox.Text;
            DateTime? dueDate = DueDatePicker.SelectedDate;

            TaskViewModel newTask = new TaskViewModel(new Task
            {
                Title = taskTitle,
                Description = taskDescription,
                DueDate = dueDate,
                SelectedPriority = GetSelectedPriority()
            });

            tasks.Add(newTask);
            ClearTaskInputs();
        }

        private void EditSelectedTask()
        {
            TaskViewModel selectedTask = TaskListBox.SelectedItem as TaskViewModel;

            if (selectedTask != null)
            {
                UpdateTask(selectedTask);
                TaskListBox.Items.Refresh();
                ClearTaskInputs();
            }
        }

        private void UpdateTask(TaskViewModel taskViewModel)
        {
            string newTitle = TaskTitleTextBox.Text;
            string newDescription = TaskDescriptionTextBox.Text;
            DateTime? newDueDate = DueDatePicker.SelectedDate;
            Priority newPriority = GetSelectedPriority();

            string originalTitle = taskViewModel.TaskModel.Title;
            string originalDescription = taskViewModel.TaskModel.Description;
            DateTime? originalDueDate = taskViewModel.TaskModel.DueDate;

            taskViewModel.TaskModel.Title = string.IsNullOrWhiteSpace(newTitle) ? originalTitle : newTitle;
            taskViewModel.TaskModel.Description = string.IsNullOrWhiteSpace(newDescription) ? originalDescription : newDescription;
            taskViewModel.TaskModel.DueDate = newDueDate ?? originalDueDate;
            taskViewModel.TaskModel.SelectedPriority = newPriority;
        }

        private void DeleteSelectedTask()
        {
            TaskViewModel selectedTask = TaskListBox.SelectedItem as TaskViewModel;
            if (selectedTask != null)
            {
                tasks.Remove(selectedTask);
                ClearTaskInputs();
            }
        }

        private Priority GetSelectedPriority()
        {
            if (PriorityComboBox.SelectedItem != null)
            {
                string selectedPriority = ((ComboBoxItem)PriorityComboBox.SelectedItem).Content.ToString();
                if (Enum.TryParse(selectedPriority, out Priority priority))
                {
                    return priority;
                }
            }

            TaskViewModel selectedTask = TaskListBox.SelectedItem as TaskViewModel;
            return selectedTask?.TaskModel.SelectedPriority ?? Priority.Medium;
        }

        private void ClearTaskInputs()
        {
            TaskTitleTextBox.Clear();
            TaskDescriptionTextBox.Clear();
            DueDatePicker.SelectedDate = null;
            PriorityComboBox.SelectedItem = null;
        }

        private void LoadTasksFromJson()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json";
            openFileDialog.Title = "Load Tasks";
            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                List<TaskViewModel> loadedTasks = taskManagerService.LoadTasksFromJson(filePath);

                if (loadedTasks != null)
                {
                    tasks.Clear();
                    foreach (var task in loadedTasks)
                    {
                        tasks.Add(task);
                    }

                    TaskListBox.ItemsSource = tasks;
                }
                else
                {
                    MessageBox.Show("No tasks found in the JSON file.");
                }
            }
        }

        private void SaveTasksToJson()
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "JSON Files (*.json)|*.json";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Title = "Save Tasks";
            saveFileDialog.FileName = "tasks.json";

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = saveFileDialog.FileName;
                List<TaskViewModel> tasksList = new List<TaskViewModel>(tasks); // Convert ObservableCollection to List
                taskManagerService.SaveTasksToJson(filePath, tasksList);
            }
        }
    }
}
