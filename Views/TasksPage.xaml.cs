using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yamah.Models;
using Yamah.ViewModels;

namespace Yamah.Views
{
    /// <summary>
    /// Interaction logic for TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        protected const string connectionString = @"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True";
        public TasksPage()
        {
            InitializeComponent();

            foreach (var task in TasksViewModel.GetAllTasks(connectionString))
                DataGridTasks.Items.Add(task);
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedRow = DataGridTasks.SelectedItem;
            ToDo task = selectedRow as ToDo;

            var user = UsersViewModel.GetUserById(connectionString, task.AssignedUserID);
            var users = UsersViewModel.GetAllUsers(connectionString);
            List<string> usernames = new List<string>();
            foreach (var u in users)
            {
                usernames.Add(u.Username);
            }


            string priority = "Low";
            string status = "New";

            if (task.PriorityID == 1)
                priority = "High";
            if (task.PriorityID == 2)
                priority = "Regular";
            if (task.PriorityID == 3)
                priority = "Low";

            if (task.StatusID == 1)
                status = "New";
            if (task.StatusID == 2)
                status = "Active";
            if (task.StatusID == 3)
                status = "Done";

            EditTaskWindow editTaskWindow = new EditTaskWindow(task, TaskPage);

            editTaskWindow.Visibility = Visibility.Visible;
            editTaskWindow.TitleBox.Text = task.Title;
            editTaskWindow.DescriptionBox.Text = task.Description;
            editTaskWindow.PriorityBox.Text = priority;
            editTaskWindow.StatusBox.Text = status;
            editTaskWindow.AssignedUserBox.ItemsSource = usernames;
            editTaskWindow.AssignedUserBox.Text = user.Username;
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedRow = DataGridTasks.SelectedItem;
            ToDo task = selectedRow as ToDo;
            if (MessageBox.Show("Are you sure you want to delete user?", "Question", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                TasksViewModel.DeleteTask(connectionString, task);
                DataGridTasks.Items.Remove(task);
            }
        }
    }
}
