using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.Models;

namespace Tidskollen.API.Dtos
{
    public class ProjectReadDto
    {        
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public ICollection<EmployeeProjectSimpleReadDto> EmployeeProject { get; set; }
    }
}
