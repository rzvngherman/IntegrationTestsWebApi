using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Data.domain;

namespace WebApplication1.Data.service
{
    public interface IEmployeeService
    {
        string GetNameById(int id);
        int Insert(string name);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetNameById(int id)
        {
            var result = _unitOfWork.EmployeeRepository.GetById(id);
            return result.Name;
        }

        public int Insert(string name)
        {
            var found = _unitOfWork.EmployeeRepository.GetByName(name);
            if(found != null)
                throw new ArgumentException("Employee already exists");

            var result = _unitOfWork.EmployeeRepository.Insert(name);
            _unitOfWork.Complete();
            return result.Id;
        }
    }
}
