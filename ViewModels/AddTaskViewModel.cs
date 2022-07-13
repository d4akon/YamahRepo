using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yamah.ViewModels
{
    public class AddTaskViewModel
    {
        public static bool IsEmpty(string title, string description, string priority, string assingedUser)
        {
            return string.IsNullOrWhiteSpace(title)
                   || string.IsNullOrWhiteSpace(description)
                   || string.IsNullOrWhiteSpace(priority)
                   || string.IsNullOrWhiteSpace(assingedUser);

        }
    }
}
