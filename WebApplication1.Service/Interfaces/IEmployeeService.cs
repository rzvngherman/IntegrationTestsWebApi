using WebApplication1.Service.Model;

namespace WebApplication1.Service.Interfaces
{
    /// <summary>
    /// Old.
    /// Should use Query / Commands (CQRS)
    /// </summary>
    public interface IEmployeeService
    {
        string GetNameById(int id);
        int Insert(EmployeeInsertModel employee);
        int TestTransaction(EmployeeInsertModel employee);
        int GetByName(string name);
    }
}
