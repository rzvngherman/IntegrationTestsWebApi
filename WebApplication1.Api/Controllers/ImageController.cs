using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data.dataaccess;
using WebApplication1.Data.domain;

namespace WebApplication1.Api.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private Dictionary<int, string> _materialImages;
        private IConfiguration _configuration;
        //public static IHostingEnvironment _environment;
        private IHostingEnvironment _environment;
        private SomeDbContext _context;

        public ImageController(IConfiguration configuration, IHostingEnvironment environment, SomeDbContext context)
        {
            _configuration = configuration;
            _environment = environment;

            _materialImages = new Dictionary<int, string>();
            _materialImages.Add(1, "/material/img_abc.jpg");
            _materialImages.Add(2, "/material/imgdasd.png");

            _context = context;
        }

        /// <summary>
        /// 1) select top 100 * from MaterialImages
        /// 2) id	image_id	image_url
        ///     1	1	        /material/img_abc.jpg
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpGet("ImageFromPath")]
        public async Task<IActionResult> GetFromPathById(int imageId)
        {
            // 'http://localhost:63161/api/image/ImageFromPath?imageId=1'
            if (!_materialImages.ContainsKey(imageId))
            {
                throw new Exception("no image with that was found");
            }

            var photo = System.IO.File.ReadAllBytes(_configuration["image_folder_path"] + _materialImages[imageId]);
            //var base64Picture = Convert.ToBase64String(photo);
            return Ok(photo);
        }

        [HttpPost("UploadDatabaseFileFromPath")]
        public async Task<IActionResult> UploadFileFromPath([FromBody] int id)
        {
            var result = await GetFromPathById(id);
            var okResult = (result as OkObjectResult).Value as byte[];
            var attachment = new Attachment
            {
                ImageContent = okResult
            };
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
            return Created("", new { id = attachment.Id });
        }







        public class AttachmentViewModel
        {
            public string File { get; set; }
        }

        /// <summary>
        /// 1) select top 100 * from MaterialImages
        /// 2) id	image_id	image_url
        ///     1	1	        /material/img_abc.jpg
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        [HttpGet("ImageFromSql")]
        public async Task<IActionResult> GetFromSqlById(int imageId)
        {
            var image = await _context
                 .Set<Attachment>()
                 .FirstOrDefaultAsync(a=>a.Id == imageId);

            if (image == null)
            {
                throw new Exception("no image was found in database");
            }

            var base64Picture = Convert.ToBase64String(image.ImageContent);
            return Ok(base64Picture);
        }

        [HttpPost("UploadDatabaseFileFromInput")]
        public async Task<ActionResult<Attachment>> PostAttachment([FromBody]AttachmentViewModel attachmentvm)
        {
            //string path = Path.Combine(_env.WebRootPath,"images", attachmentvm.FileName);
            byte[] bytes = Convert.FromBase64String(attachmentvm.File);
            var attachment = new Attachment
            {
                ImageContent = bytes,
            };
            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();
            return Created("", new { id = attachment.Id });
        }
    }
}
