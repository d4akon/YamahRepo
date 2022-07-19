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
    /// Interaction logic for AddNewTaskWindow.xaml
    /// </summary>
    public partial class AddNewTaskWindow : Window
    {
        protected const string connectionString = @"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True";
        public AddNewTaskWindow()
        {
            InitializeComponent();
        }

        private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (AddTaskViewModel.IsEmpty(TitleBox.Text, DescriptionBox.Text, PriorityBox.Text, AssignedUserBox.Text))
            {
                MessageBox.Show("Please enter values to all fields. If you need help please contact support on yamah@support.com", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int priority = 3;
                var user = UsersViewModel.GetUserByUsername(connectionString, AssignedUserBox.Text);

                if (PriorityBox.Text == "High")
                    priority = 1;
                if (PriorityBox.Text == "Regular")
                    priority = 2;
                if (PriorityBox.Text == "Low")
                    priority = 3;

                using (ToDoModel db = new ToDoModel(connectionString))
                {
                    await db.AddAsync(new ToDo
                    {
                        Title = TitleBox.Text,
                        Description = DescriptionBox.Text,
                        PriorityID = priority,
                        StatusID = 1,
                        AssignedUserID = user.ID
                    });
                    await db.SaveChangesAsync();
                }

                MessageBox.Show("New task has been added", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
