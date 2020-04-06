using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Domain;
using WebApplication1.Domain.Repository;

namespace WebApplication1.DataAccess.Repository
{
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
