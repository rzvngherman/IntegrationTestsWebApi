using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Repository
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        Employee Insert(Employee newEmployee);
        Employee GetByName(string name);

        Task<Employee> GetByNameAsync(string name);
        Task<Employee> InsertAsync(Employee employee);
    }
}
