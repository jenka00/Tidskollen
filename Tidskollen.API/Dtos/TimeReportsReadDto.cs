using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tidskollen.API.Dtos
{
    public class TimeReportsReadDto
    {
        public int ID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double HoursWorked
        {
            get
            {
                return (CheckOut - CheckIn).TotalHours;
            }
        }
        public int EmployeeId { get; set; }
        public EmployeeSimpleReadDto Employee { get; set; }
    }
}
