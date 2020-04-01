using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.domain;

namespace IntegrationTests
{
    public class TestDbData
    {
        public static void CreateData(WebApplication1.Data.dataaccess.SomeDbContext _context)
        {
            //var users = Builder<Employee>.CreateListOfSize(1000)
            //    .All()
            //    .With(c => c.Id = 0)
            //    .With(c => c.Name = Name.First())
            //    .With(c => c.Surname = Name.Last())
            //    .Build();

            _context.Employees.Add(new Employee("name01"));
            _context.Employees.Add(new Employee("name02"));
            _context.SaveChanges();
        }
    }
}
