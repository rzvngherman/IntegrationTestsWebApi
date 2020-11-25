using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<Employee> GetByNameAsync(string name)
		{
            var result = await GetQuery()
                                .FirstOrDefaultAsync(s => s.Name == name);
            if (result == null)
            {
                throw new Exception("No employee found by this name");
            }

            return result;
        }

		public async Task<Employee> InsertAsync(Employee employee)
		{
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
	}
}
