using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Service.Model;
using WebApplication1.Service.Model.Employee;

namespace WebApplication1.Service.Command
{
	public class AddEmployeeCommand : IRequest<EmployeeGetModel>
	{
		public EmployeeInsertModel Employee { get; }
		public AddEmployeeCommand(EmployeeInsertModel employee)
		{
			Employee = employee;
		}
	}
}
