using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Yamah.Models;
using Yamah.ViewModels;

namespace Yamah.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        protected const string connectionString = @"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True";

        public UsersPage()
        {
            InitializeComponent();

            foreach (var user in UsersViewModel.GetAllUsers(connectionString))
                DataGridUsers.Items.Add(user);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedRow = DataGridUsers.SelectedItem;
            User user = selectedRow as User;
            if (MessageBox.Show("Are you sure you want to delete user?", "Question", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                UsersViewModel.DeleteUser(connectionString, user);
                DataGridUsers.Items.Remove(user);
            }
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedRow = DataGridUsers.SelectedItem;
            User user = selectedRow as User;

            string roleName = "Basic";

            EditUserWindow editUserWindow = new EditUserWindow(user, UserPage);

            if (user.RoleId == 1)
                roleName = "Admin";
            if (user.RoleId == 2)
                roleName = "Manager";
            if (user.RoleId == 3)
                roleName = "Basic";

            editUserWindow.Visibility = Visibility.Visible;
            editUserWindow.UsernameBox.Text = user.Username;
            editUserWindow.EmailBox.Text = user.Email;
            editUserWindow.RoleBox.Text = roleName;
        }
    }
}