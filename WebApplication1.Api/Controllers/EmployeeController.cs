﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Data.service;

namespace WebApplication1.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
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
        public ActionResult<string> CreateEmployee([FromBody] Employee value)
        {
            var employeeId = _service.Insert(value.Name);

            var uri = Url.Link("GetNameById",
                new
                {
                    id = employeeId,
                });

            return Created(uri, new { value.Name });
        }
    }
}
