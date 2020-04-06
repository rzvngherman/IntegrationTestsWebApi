using AutoMapper;
using WebApplication1.Domain;
using WebApplication1.Service.Model;

namespace WebApplication1.Service.Mapper
{
    class EmployeeInsertModelToEmployeeConverter : ITypeConverter<EmployeeInsertModel, Employee>
    {
        public Employee Convert(EmployeeInsertModel source, Employee destination, ResolutionContext context)
        {
            var result = new Employee(source.EmployeeName, source.Age);
            return result;
        }
    }
}
