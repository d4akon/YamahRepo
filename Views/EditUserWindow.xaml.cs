using System.Linq;
using System.Windows;
using Yamah.Models;
using Yamah.ViewModels;

namespace Yamah.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        public User User { get; set; }
        public UsersPage UsersPage { get; set; }

        public EditUserWindow(User user, UsersPage usersPage)
        {
            this.UsersPage = usersPage;
            this.User = user;
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (UserModel db =
                   new UserModel(@"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True"))
            {
                int role = 3;

                if (RoleBox.Text == "Admin")
                    role = 1;
                if (RoleBox.Text == "Manager")
                    role = 2;
                if (RoleBox.Text == "Basic")
                    role = 3;

                var user = db.Users.FirstOrDefault(x => x.ID == User.ID);
                user.Username = UsernameBox.Text;
                user.Email = EmailBox.Text;
                user.RoleId = role;

                db.SaveChanges();
            }
            this.UsersPage.DataGridUsers.Items.Clear();

            foreach (var user in UsersViewModel.GetAllUsers(@"Data Source=localhost\SQLCOURSE2019;Initial Catalog=Yamah;Integrated Security=True"))
                this.UsersPage.DataGridUsers.Items.Add(user);

            this.Close();
        }
    }
}