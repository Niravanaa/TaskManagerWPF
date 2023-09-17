using System;
using System.Collections.ObjectModel;
using System.Windows;
using TaskManagementApp.ViewModels;
using TaskManagementApp.Models;
using static TaskManagementApp.Models.Task;
using System.Diagnostics;
using System.Windows.Controls;

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
            string taskTitle = TaskTitleTextBox.Text;
            string taskDescription = TaskDescriptionTextBox.Text;
            DateTime? dueDate = DueDatePicker.SelectedDate;

            TaskViewModel newTask = new TaskViewModel(new Task
            {
                Title = taskTitle,
                Description = taskDescription,
                DueDate = dueDate,
                SelectedPriority = GetSelectedPriority() // Set the task's priority
            });

            tasks.Add(newTask);

            // Clear input fields
            ClearTaskInputs();
        }


        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            // Edit the selected task
            TaskViewModel selectedTask = TaskListBox.SelectedItem as TaskViewModel;

            if (selectedTask != null)
            {
                string newTitle = TaskTitleTextBox.Text;
                string newDescription = TaskDescriptionTextBox.Text;
                DateTime? newDueDate = DueDatePicker.SelectedDate;
                Priority newPriority = GetSelectedPriority(); // Get the selected priority

                // Preserve the original values
                string originalTitle = selectedTask.TaskModel.Title;
                string originalDescription = selectedTask.TaskModel.Description;
                DateTime? originalDueDate = selectedTask.TaskModel.DueDate;

                // Update properties with non-empty values
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    selectedTask.TaskModel.Title = newTitle;
                }
                else
                {
                    selectedTask.TaskModel.Title = originalTitle; // Restore original title
                }

                if (!string.IsNullOrWhiteSpace(newDescription))
                {
                    selectedTask.TaskModel.Description = newDescription;
                }
                else
                {
                    selectedTask.TaskModel.Description = originalDescription; // Restore original description
                }

                if (newDueDate != null)
                {
                    selectedTask.TaskModel.DueDate = newDueDate;
                }
                else
                {
                    selectedTask.TaskModel.DueDate = originalDueDate; // Restore original due date
                }

                // Update the priority
                selectedTask.TaskModel.SelectedPriority = newPriority;

                // Refresh the ListBox
                TaskListBox.Items.Refresh();

                // Clear input fields
                ClearTaskInputs();
            }
        }


        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Delete the selected task
            TaskViewModel selectedTask = TaskListBox.SelectedItem as TaskViewModel;
            if (selectedTask != null)
            {
                tasks.Remove(selectedTask);

                // Clear input fields
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

            // If nothing is selected or parsing fails, return the original priority of the selected task
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
    }
}
