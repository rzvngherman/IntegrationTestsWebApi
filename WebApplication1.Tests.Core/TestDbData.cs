using WebApplication1.Data.dataaccess;
using WebApplication1.Domain;

namespace WebApplication1.Tests.Core
{
    public class TestDbData
    {
        public static string[] EmployeeNames = new string[] { "name01", "name02" };

        public static void CreateData(SomeDbContext _context)
        {
            //var users = Builder<Employee>.CreateListOfSize(1000)
            //    .All()
            //    .With(c => c.Id = 0)
            //    .With(c => c.Name = Name.First())
            //    .With(c => c.Surname = Name.Last())
            //    .Build();

            _context.Employees.Add(new Employee(EmployeeNames[0], 25));
            _context.Employees.Add(new Employee(EmployeeNames[1], 35));
            _context.SaveChanges();
        }
    }
}
