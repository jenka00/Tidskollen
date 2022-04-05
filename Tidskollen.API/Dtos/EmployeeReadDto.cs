using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tidskollen.API.Dtos
{
    public class EmployeeReadDto
    {       
        public int EmployeeId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
    }
}
