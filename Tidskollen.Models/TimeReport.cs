using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace Tidskollen.Models
{
    public class TimeReport
    {
        [Key]
        public int ID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool CheckStatus { get; set; }       
        public double HoursWorked 
        { 
            get
            {
                return (CheckOut - CheckIn).TotalHours;
            }                
        }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
