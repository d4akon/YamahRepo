using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamah.Models;

namespace Yamah.ViewModels
{
    internal class LoginViewModel
    {
        public static User? User { get; set; }
        public static bool IsCorrect(string email, string password, string connectionString)
        {
            using (UserModel db = new UserModel(connectionString))
            {
                var user = db.Users.Where(x => x.Email == email && x.Password == password);
                return user.Any();
            }
        }

        public static void GetUserByEmailAsync(string email, string connectionString)
        {
            using (UserModel db = new UserModel(connectionString))
            {
                User = db.Users.FirstOrDefault(x => x.Email == email);
            }
        }
    }
}
