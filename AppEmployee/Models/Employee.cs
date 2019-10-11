using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppEmployee.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Title")]
        [Required]
        public int TitleId { get; set; }

        [DisplayName("Date of Birth")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }
       
        [DisplayName("Sex")]
        [Required]
        public int GenderId { get; set; }
        
        [DisplayName("Location")]
        [Required]
        public int LocationId { get; set; }
        
        [DisplayName("Position")]
        [Required]
        public int PositionId { get; set; }
        [DisplayName("Age")]
        public int Age
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - DateOfBirth.Year;
                if (DateOfBirth > now.AddYears(-age)) age--;
                return age;
            }
        }

        public virtual Gender Gender { get; set; }

        public virtual Location Location { get; set; }
       
        public virtual Position Position { get; set; }

        public virtual Title Title { get; set; }

        [DisplayName("Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }     
    }
}
