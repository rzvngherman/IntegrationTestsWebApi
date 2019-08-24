using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data.domain
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }

        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
    }

    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        Employee Insert(string name);
        Employee GetByName(string name);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbContext _context;

        public EmployeeRepository(DbContext context)
        {
            _context = context;
        }

        public Employee GetById(int id)
        {
            var res = GetQuery()
                    .FirstOrDefault(s => s.Id == id);

            return res;
        }

        public Employee Insert(string name)
        {
            var newEmployee = new Employee(name);
            _context.Add(newEmployee);

            return newEmployee;
        }

        public Employee GetByName(string name)
        {
            var res = GetQuery()
                   .FirstOrDefault(s => s.Name.ToLower().Equals(name));
            return res;
        }

        private IQueryable<Employee> GetQuery()
        {
            return
                _context
                    .Set<Employee>();
        }
    }

    public class Employee
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        private Employee()
        {
        }

        public Employee(string name)
           : this()
        {
            Name = name;
        }
    }
}
