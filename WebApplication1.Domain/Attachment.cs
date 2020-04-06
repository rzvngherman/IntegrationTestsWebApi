using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication1.Domain
{
    public class Attachment
    {
        //[Key]
        public int Id { get; set; }
        public byte[] ImageContent { get; set; }

        private Attachment()
        {}

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
}
