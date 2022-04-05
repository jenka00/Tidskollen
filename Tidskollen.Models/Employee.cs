using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tidskollen.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Du måste ange ett förnamn")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Du måste ange ett efternamn")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Du måste ange ett lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Du måste upprepa ditt lösenord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Lösenorden matchar inte")]
        public string PasswordConfirmation { get; set; } 

        public ICollection<EmployeeProject> EmployeeProject { get; set; }

        public ICollection<TimeReport>TimeReport { get; set; }
    }
}
