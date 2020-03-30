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
        IAttachmentRepository AttachmentRepository { get; }

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

    public class Attachment
    {
        //[Key]
        public int Id { get; set; }
        public byte[] ImageContent { get; set; }

        private Attachment()
        {
        }

        public Attachment(byte[] content)
        {
            ImageContent = content;
        }

        public Attachment(int id, byte[] content)
        {
            Id = id;
            ImageContent = content;
        }
    }

    public interface IAttachmentRepository
    {
        Attachment Insert();
        Attachment InsertFail();
    }

    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly DbContext _context;

        public AttachmentRepository(DbContext context)
        {
            _context = context;
        }

        public Attachment Insert()
        {
            var attachment = new Attachment(new byte[] { 22, 34 });
            _context.Add(attachment);

            return attachment;
        }

        public Attachment InsertFail()
        {
            var attachment = new Attachment(1, new byte[] { 22, 34 });
            _context.Add(attachment);

            return attachment;
        }
    }
}
