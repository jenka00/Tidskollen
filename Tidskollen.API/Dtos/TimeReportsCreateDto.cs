using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tidskollen.API.Dtos
{
    public class TimeReportsCreateDto
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool CheckStatus { get; set; }
        public int EmployeeId { get; set; }       
    }
}
