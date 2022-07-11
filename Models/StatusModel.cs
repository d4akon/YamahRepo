using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Yamah.Models
{
    public class StatusModel : DbContext
    {
        public DbSet<Status> Statuses { get; set; }

        public string ConnectionString { get; }

        public StatusModel(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(this.ConnectionString);
        }
    }

    public class Status
    {
        public int ID { get; set; }
        public string Description { get; set; }
    }
}
