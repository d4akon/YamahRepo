using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Yamah.Models
{
    public class ToDoModel : DbContext
    {
        public DbSet<ToDo> ToDo { get; set; }

        public string ConnectionString { get; }

        public ToDoModel(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnectionString);
        }
    }

    public class ToDo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PriorityID { get; set; }
        public int StatusID { get; set; }
        public int AssignedUserID { get; set; }
    }
}
