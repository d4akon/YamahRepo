using System.Collections.Generic;
using System.Linq;
using Yamah.Models;

namespace Yamah.ViewModels
{
    public class UsersViewModel
    {
        public static List<User> GetAllUsers(string connectionString)
        {
            var users = new List<User>();
            using (UserModel db = new UserModel(connectionString))
            {
                foreach (var user in db.Users)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public static User GetUserById(string connectionString, int id)
        {
            var user = new User();
            using (UserModel db = new UserModel(connectionString))
            {
                user = db.Users.FirstOrDefault(x => x.ID == id);
            }

            return user;
        }

        public static User GetUserByUsername(string connectionString, string username)
        {
            var user = new User();
            using (UserModel db = new UserModel(connectionString))
            {
                user = db.Users.FirstOrDefault(x => x.Username == username);
            }

            return user;
        }

        public static void DeleteUser(string connectionString, User user)
        {
            using (UserModel db = new UserModel(connectionString))
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }
    }
}