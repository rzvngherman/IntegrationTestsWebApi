using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Api.Mapper.Converters;
using WebApplication1.Api.Models;
using WebApplication1.Service.Model;

namespace WebApplication1.Api.Mapper
{
    public class WebApplication1Profile : Profile
    {
        public WebApplication1Profile()
        {
            CreateMap<EmployeeInsertDTO, EmployeeInsertModel>().ConvertUsing<EmployeeInsertDTOToEmployeeInsertModelConverter>();
        }
    }
}
