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
        private readonly EmployeeDBContext _context;

        public EmployeesController(EmployeeDBContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employeeDBContext = _context.Employee.Include(e => e.Gender).Include(e => e.Location).Include(e => e.Position).Include(e => e.Title);
            return View(await employeeDBContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Gender)
                .Include(e => e.Location)
                .Include(e => e.Position)
                .Include(e => e.Title)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "Name");
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "Name");
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "Name");
            ViewData["TitleId"] = new SelectList(_context.Title, "TitleId", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,TitleId,DateOfBirth,GenderId,LocationId,PositionId,Age")] Employee employee)
        {           
            
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "Name", employee.GenderId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "Name", employee.LocationId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "Name", employee.PositionId);
            ViewData["TitleId"] = new SelectList(_context.Title, "TitleId", "Name", employee.TitleId);
            return View(employee);
        }

        public string GetTextFromTitleID(int titleId)
        {
            if (titleId != 0)
            {
                return _context.Title.Where(x => x.TitleId == titleId).Select(x => x.Name).FirstOrDefault();
            }

            return null;
        }
        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "Name", employee.GenderId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "Name", employee.LocationId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "Name", employee.PositionId);
            ViewData["TitleId"] = new SelectList(_context.Title, "TitleId", "Name", employee.TitleId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,TitleId,DateOfBirth,GenderId,LocationId,PositionId,Age")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
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
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "Name", employee.GenderId);
            ViewData["LocationId"] = new SelectList(_context.Location, "LocationId", "Name", employee.LocationId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "Name", employee.PositionId);
            ViewData["TitleId"] = new SelectList(_context.Title, "TitleId", "Name", employee.TitleId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Gender)
                .Include(e => e.Location)
                .Include(e => e.Position)
                .Include(e => e.Title)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}
