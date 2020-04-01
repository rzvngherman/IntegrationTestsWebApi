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
        int TestTransaction(string name);
        int GetByName(string name);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int GetByName(string name)
        {
            var result = _unitOfWork.EmployeeRepository.GetByName(name);
            return result.Id;
        }

        public string GetNameById(int id)
        {
            var result = _unitOfWork.EmployeeRepository.GetById(id);
            return result.Name;
        }

        public int Insert(string name)
        {
            //_unitOfWork.co
            var found = _unitOfWork.EmployeeRepository.GetByName(name);
            if(found != null)
                throw new ArgumentException("Employee already exists");

            var result = _unitOfWork.EmployeeRepository.Insert(name);
            _unitOfWork.Complete();
            return result.Id;
        }

        public int TestTransaction(string name)
        {           
            var result = _unitOfWork.EmployeeRepository.Insert(name);
            var result2 = _unitOfWork.AttachmentRepository.Insert();
            var result3 = _unitOfWork.AttachmentRepository.InsertFail();

            _unitOfWork.Complete();
            return result.Id;
        }
    }
}
