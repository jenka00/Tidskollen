using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Dtos;
using Tidskollen.Models;

namespace Tidskollen.API.Profiles
{
    public class TimeReportsProfile : Profile
    {
        public TimeReportsProfile()
        {
            CreateMap<TimeReport, TimeReportsReadDto>();
            CreateMap<TimeReportsCreateDto, TimeReport>();
        }
    }
}
