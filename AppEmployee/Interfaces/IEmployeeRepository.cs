using AppEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppEmployee.Interfaces
{
    public interface IEmployeeRepository : IDisposable
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeByID(int empId);
        void InsertEmployee(Employee emp);
        void DeleteEmployee(int empId);
        void UpdateEmployee(Employee employee);
        List<Gender> GetGender();
        List<Location> GetLocation();
        List<Title> GetTitle();
        List<Position> GetPosition();
        void Save();
    }
}
