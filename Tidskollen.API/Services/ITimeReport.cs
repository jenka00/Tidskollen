using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.Models;

namespace Tidskollen.API.Services
{
    public interface ITimeReport
    {
        Task <IEnumerable<TimeReport>> GetWorkHours(int employeeId, DateTime startDate, DateTime endDate);
        Task <IEnumerable<TimeReport>> GetByEmpID(int employeeId);
    }
}