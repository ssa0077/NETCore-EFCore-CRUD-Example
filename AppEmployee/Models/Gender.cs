﻿using System;
using System.Collections.Generic;

namespace AppEmployee.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Employee = new HashSet<Employee>();
        }

        public int GenderId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }
    }
}
