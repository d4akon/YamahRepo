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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private string connectionString =
            @"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True";

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (RegisterViewModel.IsEmpty(UsernameBox.Text, EmailBox.Text, PasswordBox.Password))
            {
                MessageBox.Show("Please enter values to all fields. If you need help please contact support on yamah@support.com", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (await RegisterViewModel.IsExistingAsync(EmailBox.Text, connectionString))
            {
                MessageBox.Show("An account is already registered with your email address. Please login or use another email address", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Visibility = Visibility.Visible;
                Window win = (Window)this.Parent;
                win.Close();
            }
            else
            {
                using (UserModel db = new UserModel(connectionString))
                {
                    await db.AddAsync(new User { Username = UsernameBox.Text, Email = EmailBox.Text, Password = PasswordBox.Password, RoleId = 3 });
                    await db.SaveChangesAsync();
                }

                MessageBox.Show("New user has been created", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Visibility = Visibility.Visible;
                Window win = (Window)this.Parent;
                win.Close();
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }


        private void UsernameBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text) || tb.Text == "Username")
                tb.Text = string.Empty;
        }

        private void UsernameBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
                tb.Text = "Username";
        }

        private void EmailBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text) || tb.Text == "E-mail")
                tb.Text = string.Empty;
        }

        private void EmailBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
                tb.Text = "E-mail";
        }

        private void PasswordBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            if (string.IsNullOrWhiteSpace(pb.Password) || pb.Password == "password")
                pb.Password = string.Empty;
        }

        private void PasswordBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            if (string.IsNullOrWhiteSpace(pb.Password))
                pb.Password = "password";
        }


    }
}
