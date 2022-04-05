using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.Models;

namespace Tidskollen.API.Dtos
{
    public class EmployeeProjectReadDto
    {        
        public int EmployeeProjectId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeReadDto Employee { get; set; }

        public int ProjectId { get; set; }
        public ProjectReadDto Project { get; set; }
    }
}
