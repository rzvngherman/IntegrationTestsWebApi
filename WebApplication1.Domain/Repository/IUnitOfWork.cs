using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Repository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IAttachmentRepository AttachmentRepository { get; }

        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
    }
}
