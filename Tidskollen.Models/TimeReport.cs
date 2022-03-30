using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tidskollen.Models
{
    public class TimeReport
    {
        [Key]
        public int ID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool CheckStatus { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
