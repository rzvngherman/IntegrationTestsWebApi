using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Domain;
using WebApplication1.Service.Model;

namespace WebApplication1.Service.Mapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<EmployeeInsertModel, Employee>().ConvertUsing<EmployeeInsertModelToEmployeeConverter>();
        }
    }

    public class ConfigureMapper
    {
        public static IMapper CreateMapper(Profile customProfile = null)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperConfig());
                if (customProfile != null)
                    cfg.AddProfile(customProfile);
            });

            return config.CreateMapper();
        }
    }
}
