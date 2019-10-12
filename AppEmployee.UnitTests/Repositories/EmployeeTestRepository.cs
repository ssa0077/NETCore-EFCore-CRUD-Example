using AppEmployee.Interfaces;
using AppEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppEmployee.UnitTests.Repositories
{
    class EmployeeTestRepository : IEmployeeRepository
    {
        List<Employee> employees = null;

        public EmployeeTestRepository(List<Employee> emps)
        {
            employees = emps;
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public void InsertEmployee(Employee emp)
        {
            employees.Add(emp);
        }

        public void UpdateEmployee(Employee emp)
        {
            int id = emp.EmployeeId;
            Employee empToUpdate = employees.SingleOrDefault(x => x.EmployeeId == id);
            DeleteEmployee(empToUpdate.EmployeeId);
            employees.Add(emp);
        }

        public void DeleteEmployee(int empId)
        {
            Employee employee = employees.Where(x => x.EmployeeId == empId).FirstOrDefault();
            employees.Remove(employee);
        }

        public Employee GetEmployeeByID(int id)
        {
            return employees.SingleOrDefault(m => m.EmployeeId == id);
        }

        public void Save()
        {
            //Nothing to implement here.
        }

        public List<Gender> GetGender()
        {
            var gender = new List<Gender>()
            {
                new Gender {GenderId = 1, Name = "Male"},
                new Gender {GenderId = 2, Name = "Female"}
            };

            return gender;
        }

        public List<Title> GetTitle()
        {
            var title = new List<Title>()
            {
                new Title { TitleId = 1, Name = "Mr"},
                new Title {TitleId = 2, Name = "Mrs"},
                new Title {TitleId = 3, Name = "Miss"},
                new Title {TitleId = 4, Name = "Dr"}
            };

            return title;
        }

        public List<Location> GetLocation()
        {
            var location = new List<Location>()
            {
                new Location { LocationId = 1, Name = "London"},
                new Location {LocationId = 2, Name = "Glasgow"},
                new Location {LocationId = 3, Name = "Sydney"},
                new Location {LocationId = 4, Name = "Melbourne"}
            };

            return location;
        }

        public List<Position> GetPosition()
        {
            var position = new List<Position>()
            {
                new Position { PositionId = 1, Name = "Business Analyst"},
                new Position {PositionId = 2, Name = "Business Manager"},
                new Position {PositionId = 3, Name = "Risk Manager"},
                new Position {PositionId = 4, Name = "Accounts Analyst"}
            };

            return position;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
