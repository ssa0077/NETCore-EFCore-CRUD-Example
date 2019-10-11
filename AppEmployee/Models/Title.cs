using System;
using System.Collections.Generic;

namespace AppEmployee.Models
{
    public partial class Title
    {
        public Title()
        {
            Employee = new HashSet<Employee>();
        }

        public int TitleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
