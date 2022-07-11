using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Yamah.Models
{
    public class RoleModel : DbContext
    {
        public DbSet<Role> Roles { get; set; }

        public string ConnectionString { get; }

        public RoleModel(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnectionString);
        }
    }

    public class Role
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }
}
