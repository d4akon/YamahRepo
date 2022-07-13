using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamah.Models;

namespace Yamah.ViewModels
{
    public class TasksViewModel
    {
        public static List<ToDo> GetAllTasks(string connectionString)
        {
            var tasks = new List<ToDo>();
            using (ToDoModel db = new ToDoModel(connectionString))
            {
                foreach (var task in db.ToDo)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }

        public static void DeleteTask(string connectionString, ToDo task)
        {
            using (ToDoModel db = new ToDoModel(connectionString))
            {
                db.ToDo.Remove(task);
                db.SaveChanges();
            }
        }
    }
}
