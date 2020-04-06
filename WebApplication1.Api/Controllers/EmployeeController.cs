using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Api.Models;
using WebApplication1.Service.Interfaces;
using WebApplication1.Service.Model;

namespace WebApplication1.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get employee id by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetIdByName/{name}", Name = "GetIdByName")]
        public ActionResult<int> GetEmployeeId(string name)
        {
            var result = _service.GetByName(name);

            return Ok(new { Id = result });
        }

        /// <summary>
        /// Get employee name by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetNameById/{id}", Name = "GetNameById")]
        public ActionResult<string> GetEmployeeName(int id)
        {
            var result = _service.GetNameById(id);

            return Ok(new { Name = result });
        }

        /// <summary>
        /// Insert employee
        /// </summary>
        /// <returns></returns>
        [HttpPost("Insert")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> CreateEmployee([FromBody] EmployeeInsertDTO employee)
        {
            var newEmployee = _mapper.Map<EmployeeInsertModel>(employee);
            var employeeId = _service.Insert(newEmployee);

            var uri = Url.Link("GetNameById",
                new
                {
                    id = employeeId,
                });
            return Created(uri, new { employee.Name });
        }
    }

    public class GetEmployeeModel
    {
        public string Name { get; set; }
    }
}
