using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Data.domain;

namespace WebApplication1.Data.dataaccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SomeDbContext _context;

        public UnitOfWork(SomeDbContext context/*, UnitOfWorkConfiguration unitOfWorkConfiguration*/)
        {
            _context = context;
            EmployeeRepository = new EmployeeRepository(context);
            AttachmentRepository = new AttachmentRepository(context);
        }

        public IEmployeeRepository EmployeeRepository { get; }
        public IAttachmentRepository AttachmentRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
