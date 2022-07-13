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
using System.Windows.Shapes;
using Yamah.Models;
using Yamah.ViewModels;

namespace Yamah.Views
{
    /// <summary>
    /// Interaction logic for EditTaskWindow.xaml
    /// </summary>
    public partial class EditTaskWindow : Window
    {
        private readonly string connectionString =
            @"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True";

        public ToDo Task { get; set; }
        public TasksPage TasksPage { get; set; }

        public EditTaskWindow(ToDo task, TasksPage tasksPage)
        {
            this.TasksPage = tasksPage;
            this.Task = task;
            InitializeComponent();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (ToDoModel db =
                   new ToDoModel(connectionString))
            {
                int priority = 3;
                int status = 1;
                var user = UsersViewModel.GetUserByUsername(connectionString, AssignedUserBox.Text);

                if (PriorityBox.Text == "High")
                    priority = 1;
                if (PriorityBox.Text == "Regular")
                    priority = 2;
                if (PriorityBox.Text == "Low")
                    priority = 3;

                if (StatusBox.Text == "New")
                    status = 1;
                if (StatusBox.Text == "Active")
                    status = 2;
                if (StatusBox.Text == "Done")
                    status = 3;

                var task = db.ToDo.FirstOrDefault(x => x.ID == Task.ID);
                task.Title = TitleBox.Text;
                task.Description = DescriptionBox.Text;
                task.PriorityID = priority;
                task.StatusID = status;
                task.AssignedUserID = user.ID;

                db.SaveChanges();
            }
            this.TasksPage.DataGridTasks.Items.Clear();

            foreach (var task in TasksViewModel.GetAllTasks(@"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True"))
                this.TasksPage.DataGridTasks.Items.Add(task);

            this.Close();
        }
    }
}
