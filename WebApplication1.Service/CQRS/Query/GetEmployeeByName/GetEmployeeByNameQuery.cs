using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Service.Model.Employee;

namespace WebApplication1.Service.Query
{
	public class GetEmployeeByNameQuery : IRequest<EmployeeGetModel>
	{
		public string Name { get; }

		public GetEmployeeByNameQuery(string employeeName)
		{
			Name = employeeName;
		}
	}
}
