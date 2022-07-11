using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Yamah.Models;

namespace Yamah.ViewModels
{
    internal class RegisterViewModel
    {
        public static async Task<bool> IsExistingAsync(string email, string connectionString)
        {
            using (UserModel db = new UserModel(connectionString))
            {
                return await db.Users.AnyAsync(x => x.Email == email);
            }
        }

        public static bool IsEmpty(string username, string email, string password)
        {
            return string.IsNullOrWhiteSpace(username)
                   || string.IsNullOrWhiteSpace(email)
                   || string.IsNullOrWhiteSpace(password)
                   || username == "Username"
                   || email == "E-Mail";

        }
    }
}
