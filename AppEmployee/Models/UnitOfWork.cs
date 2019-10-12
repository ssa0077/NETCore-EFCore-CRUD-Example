using AppEmployee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppEmployee.Models
{
    public class UnitOfWork : IDisposable
    {
        private EmployeeDBContext context = null;

        public UnitOfWork()
        {
            context = new EmployeeDBContext();
            EmployeeRepository = new EmployeeRepository(context);
        }

        public UnitOfWork(IEmployeeRepository employeeRepository)
        {
            EmployeeRepository = employeeRepository;
        }

        public IEmployeeRepository EmployeeRepository
        {
            get;
            private set;
        }        

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                context = null;
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

    }
}
