using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Domain;
using WebApplication1.Domain.Repository;

namespace WebApplication1.DataAccess.Repository
{
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

        public Employee Insert(Employee newEmployee)
        {
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
}
