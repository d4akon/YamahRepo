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

namespace Yamah.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string connectionString =
            @"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (UserModel db = new UserModel(connectionString))
            {
                var check = LoginViewModel.IsCorrect(EmailBox.Text, PasswordBox.Password, connectionString);

                if (check)
                {
                    LoginViewModel.GetUserByEmailAsync(EmailBox.Text, connectionString);
                    var menuWindow = new MenuWindow();
                    menuWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong email or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            this.Content = registerPage;
        }

        private void EmailBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
                tb.Text = "E-mail";
        }

        private void EmailBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text) || tb.Text == "E-mail")
                tb.Text = string.Empty;
        }

        private void PasswordBoxOnClick_GetFocus(object sender, RoutedEventArgs e)
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
