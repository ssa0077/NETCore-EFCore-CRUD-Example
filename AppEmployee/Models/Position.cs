using System;
using System.Collections.Generic;

namespace AppEmployee.Models
{
    public partial class Position
    {
        public Position()
        {
            Employee = new HashSet<Employee>();
        }

        public int PositionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
