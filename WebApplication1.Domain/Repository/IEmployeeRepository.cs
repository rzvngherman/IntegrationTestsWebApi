using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Domain.Repository
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        Employee Insert(Employee newEmployee);
        Employee GetByName(string name);
    }
}
