using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Api.Models;
using WebApplication1.Service.Command;
using WebApplication1.Service.Model;
using WebApplication1.Service.Query;

namespace WebApplication1.Api.Controllers
{
	[Route("api/[controller]")]
	public class EmployeeCQRSController : Controller
	{
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EmployeeCQRSController(IMediator mediator, IMapper mapper)
		{
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Get employee by name
        /// </summary>        
        /// <returns></returns>
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var orderDetails = await _mediator.Send(new GetEmployeeByNameQuery(name));
            return Ok(orderDetails);
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
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeInsertDTO employee)
        {
            var newEmployee = _mapper.Map<EmployeeInsertModel>(employee);
            var result = await _mediator.Send(new AddEmployeeCommand(newEmployee));

            return Created(string.Empty, new { result.Id, EmployeeName = result.Name, EmployeeAge = result.Age });
        }
    }
}
