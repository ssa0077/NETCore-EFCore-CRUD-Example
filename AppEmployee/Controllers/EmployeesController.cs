using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppEmployee.Models;

namespace AppEmployee.Controllers
{
    public class EmployeesController : Controller
    {
        private UnitOfWork unitOfWork = null;        

        public EmployeesController(UnitOfWork uow)
        {
            unitOfWork = uow;
        }

        // GET: Employees
        public IActionResult Index()
        {
            List<Employee> employees = unitOfWork.EmployeeRepository.GetEmployees();
            foreach(Employee emp in employees)
            {
                emp.Title = GetTitle(emp.TitleId);
                emp.Location = GetLocation(emp.LocationId);
                emp.Position = GetPosition(emp.PositionId);
                emp.Gender = GetGender(emp.GenderId);
            }
            return View(employees.ToList());
        }

        public Title GetTitle(int id)
        {
            return unitOfWork.EmployeeRepository.GetTitle().Where(x => x.TitleId == id).FirstOrDefault();
        }

        public Location GetLocation(int id)
        {
            return unitOfWork.EmployeeRepository.GetLocation().Where(x => x.LocationId == id).FirstOrDefault();
        }

        public Position GetPosition(int id)
        {
            return unitOfWork.EmployeeRepository.GetPosition().Where(x => x.PositionId == id).FirstOrDefault();
        }

        public Gender GetGender(int id)
        {
            return unitOfWork.EmployeeRepository.GetGender().Where(x => x.GenderId == id).FirstOrDefault();
        }

        // GET: Employees/Details/5
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Employee employee = unitOfWork.EmployeeRepository.GetEmployeeByID(id);         
    
            if (employee == null)
            {
                return NotFound();
            }
            employee.Gender = GetGender(employee.GenderId);
            employee.Position = GetPosition(employee.PositionId);
            employee.Location = GetLocation(employee.LocationId);
            employee.Title = GetTitle(employee.TitleId);
            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(unitOfWork.EmployeeRepository.GetGender(), "GenderId", "Name");
            ViewData["LocationId"] = new SelectList(unitOfWork.EmployeeRepository.GetLocation(), "LocationId", "Name");
            ViewData["PositionId"] = new SelectList(unitOfWork.EmployeeRepository.GetPosition(), "PositionId", "Name");
            ViewData["TitleId"] = new SelectList(unitOfWork.EmployeeRepository.GetTitle(), "TitleId", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EmployeeId,FirstName,LastName,TitleId,DateOfBirth,GenderId,LocationId,PositionId,Age")] Employee employee)
        {           
            
            if (ModelState.IsValid)
            {
                unitOfWork.EmployeeRepository.InsertEmployee(employee);
                unitOfWork.EmployeeRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(unitOfWork.EmployeeRepository.GetGender(), "GenderId", "Name", employee.GenderId);
            ViewData["LocationId"] = new SelectList(unitOfWork.EmployeeRepository.GetLocation(), "LocationId", "Name", employee.LocationId);
            ViewData["PositionId"] = new SelectList(unitOfWork.EmployeeRepository.GetPosition(), "PositionId", "Name", employee.PositionId);
            ViewData["TitleId"] = new SelectList(unitOfWork.EmployeeRepository.GetTitle(), "TitleId", "Name", employee.TitleId);
            return View(employee);
        }
       
        // GET: Employees/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employee =  unitOfWork.EmployeeRepository.GetEmployeeByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(unitOfWork.EmployeeRepository.GetGender(), "GenderId", "Name", employee.GenderId);
            ViewData["LocationId"] = new SelectList(unitOfWork.EmployeeRepository.GetLocation(), "LocationId", "Name", employee.LocationId);
            ViewData["PositionId"] = new SelectList(unitOfWork.EmployeeRepository.GetPosition(), "PositionId", "Name", employee.PositionId);
            ViewData["TitleId"] = new SelectList(unitOfWork.EmployeeRepository.GetTitle(), "TitleId", "Name", employee.TitleId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("EmployeeId,FirstName,LastName,TitleId,DateOfBirth,GenderId,LocationId,PositionId,Age")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.EmployeeRepository.UpdateEmployee(employee);
                    unitOfWork.EmployeeRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(unitOfWork.EmployeeRepository.GetGender(), "GenderId", "Name", employee.GenderId);
            ViewData["LocationId"] = new SelectList(unitOfWork.EmployeeRepository.GetLocation(), "LocationId", "Name", employee.LocationId);
            ViewData["PositionId"] = new SelectList(unitOfWork.EmployeeRepository.GetPosition(), "PositionId", "Name", employee.PositionId);
            ViewData["TitleId"] = new SelectList(unitOfWork.EmployeeRepository.GetTitle(), "TitleId", "Name", employee.TitleId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var employee = unitOfWork.EmployeeRepository.GetEmployeeByID(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Gender = GetGender(employee.GenderId);
            employee.Position = GetPosition(employee.PositionId);
            employee.Location = GetLocation(employee.LocationId);
            employee.Title = GetTitle(employee.TitleId);

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = unitOfWork.EmployeeRepository.GetEmployeeByID(id);
            unitOfWork.EmployeeRepository.DeleteEmployee(id);
            unitOfWork.EmployeeRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return unitOfWork.EmployeeRepository.GetEmployees().Any(e => e.EmployeeId == id);
        }
    }
}
