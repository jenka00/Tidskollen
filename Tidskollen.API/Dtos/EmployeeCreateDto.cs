using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tidskollen.API.Dtos
{
    public class EmployeeCreateDto
    {
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
    }
}
