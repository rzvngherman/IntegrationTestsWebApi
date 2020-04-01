using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "(local) api.WebApplication1.Api value1", "(local) api.WebApplication1.Api value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return $"(local) value for {id}";
        }

        //// POST api/values
        //[Route("insert_value")]
        //[HttpPost]
        //public ActionResult<string> Post([FromBody] Employee value)
        //{
        //    //return Ok(new { value.LastName, value.FirstName });
        //    return Ok("inserted");
        //}

        //// POST api/values
        //[Route("insert_employee")]
        //[HttpPost]
        //public ActionResult<string> InsertEmployee([FromBody] Employee value)
        //{
        //    return Ok(new { value.LastName, value.FirstName });
        //}

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        // GET api/values/employee/5

    }
}
