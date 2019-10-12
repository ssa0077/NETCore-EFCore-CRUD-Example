using AppEmployee.Controllers;
using AppEmployee.Models;
using AppEmployee.UnitTests.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppEmployee.UnitTests.Controllers
{

    [TestClass]
    public class EmployeeTestController
    {
        Employee emp1 = null;
        Employee emp2 = null;
        Employee emp3 = null;
        Employee emp4 = null;
        Employee emp5 = null;

        List<Employee> emps = null;
        EmployeeTestRepository empRepository = null;
        EmployeesController controller = null;
        UnitOfWork uow = null;

        public EmployeeTestController()
        {
            emp1 = new Employee { EmployeeId = 11, FirstName = "Joe", LastName = "Bloggs", Gender = null, Location = null, Position = null, Title = null, TitleId = 1, GenderId = 1, LocationId = 2, PositionId = 2, DateOfBirth = new DateTime(1969, 09, 09) };
            emp2 = new Employee { EmployeeId = 12, FirstName = "Peter", LastName = "Broad", Gender = null, Location = null, Position = null, Title = null, TitleId = 4, GenderId = 1, LocationId = 3, PositionId = 1, DateOfBirth = new DateTime(1949, 09, 09) };
            emp3 = new Employee { EmployeeId = 13, FirstName = "Ellie", LastName = "Tyler", Gender = null, Location = null, Position = null, Title = null, TitleId = 3, GenderId = 2, LocationId = 3, PositionId = 3, DateOfBirth = new DateTime(1989, 09, 09) };
            emp4 = new Employee { EmployeeId = 14, FirstName = "James", LastName = "Maddison", Gender = null, Location = null, Position = null, Title = null, TitleId = 1, GenderId = 1, LocationId = 1, PositionId = 1, DateOfBirth = new DateTime(1979, 09, 09) };
            emp5 = new Employee { EmployeeId = 15, FirstName = "Sarah", LastName = "Newman", Gender = null, Location = null, Position = null, Title = null, TitleId = 2, GenderId = 2, LocationId = 2, PositionId = 4, DateOfBirth = new DateTime(1959, 09, 09) };

            emps = new List<Employee>
            {
                emp1,
                emp2,
                emp3,
                emp4,
                emp5
            };

            empRepository = new EmployeeTestRepository(emps);
            uow = new UnitOfWork(empRepository);
            controller = new EmployeesController(uow);
        }

        [TestMethod]
        public void Index()
        {
            ViewResult result = controller.Index() as ViewResult;
            var model = (List<Employee>)result.ViewData.Model;

            CollectionAssert.Contains(model, emp1);
            CollectionAssert.Contains(model, emp2);
            CollectionAssert.Contains(model, emp3);
            CollectionAssert.Contains(model, emp4);
            CollectionAssert.Contains(model, emp5);
        }

        [TestMethod]
        public void Details()
        {
            ViewResult result = controller.Details(11) as ViewResult;
            Assert.AreEqual(result.Model, emp1);
        }

        [TestMethod]
        public void Create()
        {
            Employee newEmployee = new Employee { EmployeeId = 11, FirstName = "newFirstName", LastName = "newLastName", TitleId = 1, GenderId = 1, LocationId = 2, PositionId = 3, DateOfBirth = new DateTime(1999, 09, 09) };
            controller.Create(newEmployee);
            List<Employee> emps = empRepository.GetEmployees();

            CollectionAssert.Contains(emps, newEmployee);
        }

        [TestMethod]
        public void Edit()
        {
            Employee editEmployee = empRepository.GetEmployeeByID(11);
            editEmployee.FirstName = "New";
            editEmployee.LastName = "Employee";
            editEmployee.TitleId = 3;
            editEmployee.GenderId = 2;
            editEmployee.LocationId = 3;
            editEmployee.PositionId = 1;
            editEmployee.DateOfBirth = new DateTime(1999, 09, 09);
            controller.Edit(editEmployee.EmployeeId, editEmployee);

            List<Employee> emps = empRepository.GetEmployees();
            CollectionAssert.Contains(emps, editEmployee);
        }

        [TestMethod]
        public void Delete()
        {
            controller.Delete(11);
            controller.DeleteConfirmed(11);
            List<Employee> emps = empRepository.GetEmployees();

            CollectionAssert.DoesNotContain(emps, emp1);
        }

        public Title GetTitle(int id)
        {
            return empRepository.GetTitle().Where(x => x.TitleId == id).FirstOrDefault();
        }

        public Location GetLocation(int id)
        {
            return empRepository.GetLocation().Where(x => x.LocationId == id).FirstOrDefault();
        }

        public Position GetPosition(int id)
        {
            return empRepository.GetPosition().Where(x => x.PositionId == id).FirstOrDefault();
        }

        public Gender GetGender(int id)
        {
            return empRepository.GetGender().Where(x => x.GenderId == id).FirstOrDefault();
        }       
    }
}

