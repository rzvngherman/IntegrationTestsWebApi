using WebApplication1.Service.Model;

namespace WebApplication1.Service.Interfaces
{
    public interface IEmployeeService
    {
        string GetNameById(int id);
        int Insert(EmployeeInsertModel employee);
        int TestTransaction(EmployeeInsertModel employee);
        int GetByName(string name);
    }
}
