using AutoMapper;
using WebApplication1.Api.Models;
using WebApplication1.Service.Model;

namespace WebApplication1.Api.Mapper.Converters
{
    public class EmployeeInsertDTOToEmployeeInsertModelConverter : ITypeConverter<EmployeeInsertDTO, EmployeeInsertModel>
    {
        public EmployeeInsertModel Convert(EmployeeInsertDTO source, EmployeeInsertModel destination, ResolutionContext context)
        {
            var result = new EmployeeInsertModel()
            {
                EmployeeName = source.Name,
                Age = source.Age,
            };

            return result;
        }
    }
}
