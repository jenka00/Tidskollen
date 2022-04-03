using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tidskollen.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public ICollection<EmployeeProject> EmployeeProject { get; set; }

        public ICollection<TimeReport>TimeReport { get; set; }
    }
}
