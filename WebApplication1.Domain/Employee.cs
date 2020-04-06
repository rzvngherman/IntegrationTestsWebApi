using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Domain
{
    public class Employee
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }

        private Employee()
        {}

        public Employee(string name, int age)
           : this()
        {
            Name = name;
            Age = age;
        }
    }
}
