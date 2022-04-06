using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Dtos;
using Tidskollen.Models;

namespace Tidskollen.API.Profiles
{
    public class EmployeeProjectsProfile : Profile
    {
        public EmployeeProjectsProfile()
        {
            CreateMap<EmployeeProject, EmployeeProjectReadDto>();
            CreateMap<EmployeeProject, EmployeeProjectSimpleReadDto>();
        }
    }
}
