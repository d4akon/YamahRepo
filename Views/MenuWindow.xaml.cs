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
using Yamah.Views;
using Yamah.ViewModels;

namespace Yamah.Views
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        protected const string connectionString = @"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True";
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void MenuWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void UsersButton_OnClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new UsersPage();
        }

        private void TasksButton_OnClick(object sender, RoutedEventArgs e)
        {
            Main.Content = new TasksPage();
        }

        private void LogoutButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            this.Close();
        }

        private void AddNewTaskButton_OnClick(object sender, RoutedEventArgs e)
        {
            var users = UsersViewModel.GetAllUsers(connectionString);
            List<string> usernames = new List<string>();
            foreach (var u in users)
            {
                usernames.Add(u.Username);
            }

            AddNewTaskWindow addNewTaskWindow = new AddNewTaskWindow();

            addNewTaskWindow.Visibility = Visibility.Visible;
            addNewTaskWindow.AssignedUserBox.ItemsSource = usernames;
        }
    }
}