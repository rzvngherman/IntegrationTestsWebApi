using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Domain.Repository;
using WebApplication1.Service.Interfaces;
using WebApplication1.Service.Model;

namespace WebApplication1.Service
{
    /// <summary>
    /// Old.
    /// Should use Query / Commands (CQRS)
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public int GetByName(string name)
        {
            var result = _unitOfWork.EmployeeRepository.GetByName(name);
            //can be null

            return result.Id;
        }

        public string GetNameById(int id)
        {
            var result = _unitOfWork.EmployeeRepository.GetById(id);
            return result.Name;
        }

        public int Insert(EmployeeInsertModel employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            var found = _unitOfWork.EmployeeRepository.GetByName(employee.EmployeeName);
            if (found != null)
            {
                throw new ArgumentException("Employee already exists");
            }

            var toInsert = _mapper.Map<Domain.Employee>(employee);

            var result = _unitOfWork.EmployeeRepository.Insert(toInsert);
            _unitOfWork.Complete();
            return result.Id;
        }

        public int TestTransaction(EmployeeInsertModel employee)
        {
            var result = _unitOfWork.EmployeeRepository.Insert(new Domain.Employee(employee.EmployeeName, employee.Age));
            var result2 = _unitOfWork.AttachmentRepository.Insert();
            var result3 = _unitOfWork.AttachmentRepository.InsertFail();

            _unitOfWork.Complete();
            return result.Id;
        }
    }
}
