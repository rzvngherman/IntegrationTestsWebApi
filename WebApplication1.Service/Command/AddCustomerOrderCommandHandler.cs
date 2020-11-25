using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Domain;
using WebApplication1.Domain.Repository;
using WebApplication1.Service.Model.Employee;

namespace WebApplication1.Service.Command
{
	public class AddCustomerOrderCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeGetModel>
	{
		private readonly IUnitOfWork _unitOfWork;

		public AddCustomerOrderCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<EmployeeGetModel> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
		{
			var employee = new Employee(request.Employee.EmployeeName, request.Employee.Age);
			var insertedEmployee = await _unitOfWork.EmployeeRepository.InsertAsync(employee);

			var result = new EmployeeGetModel
			{
				Id = insertedEmployee.Id,
				Name = insertedEmployee.Name,
				Age = insertedEmployee.Age
			};

			return result;
		}
	}
}
