using AppEmployee.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppEmployee.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeDBContext _context = null;

        public EmployeeRepository(EmployeeDBContext employeeContext)
        {
            _context = employeeContext;
        }

        public List<Employee> GetEmployees()
        {
            return _context.Employee.ToList();
        }

        public List<Title> GetTitle()
        {
            return _context.Title.ToList();
        }

        public List<Gender> GetGender()
        {
            return _context.Gender.ToList();
        }

        public List<Position> GetPosition()
        {
            return _context.Position.ToList();
        }

        public List<Location> GetLocation()
        {
            return _context.Location.ToList();
        }

        public Employee GetEmployeeByID(int id)
        {
            return _context.Employee.Find(id);
        }

        public void InsertEmployee(Employee patient)
        {
            _context.Employee.Add(patient);
        }

        public void DeleteEmployee(int empId)
        {
            Employee emp = _context.Employee.Find(empId);
            _context.Employee.Remove(emp);
        }

        public void UpdateEmployee(Employee emp)
        {
            _context.Entry(emp).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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
