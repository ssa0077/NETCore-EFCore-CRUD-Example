using System;
using System.Collections.Generic;

namespace AppEmployee.Models
{
    public partial class Location
    {
        public Location()
        {
            Employee = new HashSet<Employee>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
