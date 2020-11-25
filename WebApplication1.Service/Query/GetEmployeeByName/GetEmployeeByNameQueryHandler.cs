using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Domain.Repository;
using WebApplication1.Service.Model.Employee;

namespace WebApplication1.Service.Query
{
	public class GetEmployeeByNameQueryHandler : IRequestHandler<GetEmployeeByNameQuery, EmployeeGetModel>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetEmployeeByNameQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<EmployeeGetModel> Handle(GetEmployeeByNameQuery request, CancellationToken cancellationToken)
		{
			var domainEmployee = await _unitOfWork.EmployeeRepository.GetByNameAsync(request.Name);
			var result = new EmployeeGetModel
			{
				Id = domainEmployee.Id,
				Name = domainEmployee.Name,
				Age = domainEmployee.Age
			};
			return result;
		}
	}
}
