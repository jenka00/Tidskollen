using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Model;
using Tidskollen.Models;

namespace Tidskollen.API.Services
{
    public class TimeReportRepo : ITidskollen<TimeReport>, ITimeReport
    {
        private TidDbContext _tidContext;

        public TimeReportRepo(TidDbContext tidContext)
        {
            _tidContext = tidContext;
        }

        public async Task<IEnumerable<TimeReport>> GetAll()
        {
            return await _tidContext.TimeReports.Include(e => e.Employee).ToListAsync();
        }

        public async Task<TimeReport> GetSingle(int id)
        {
            return await _tidContext.TimeReports.Include(e => e.Employee)
                .FirstOrDefaultAsync(tr => tr.ID == id);
        }

        public async Task<TimeReport> Add(TimeReport newEntity)
        {
            var timeRepToAdd = await _tidContext.TimeReports.AddAsync(newEntity);
            await _tidContext.SaveChangesAsync();
            return timeRepToAdd.Entity;
        }

        public async Task<TimeReport> Delete(int id)
        {
            var timeRepToDelete = await _tidContext.TimeReports.Where(tr => tr.ID == id)
                .FirstOrDefaultAsync();
            if (timeRepToDelete != null)
            {
                _tidContext.TimeReports.Remove(timeRepToDelete);
                await _tidContext.SaveChangesAsync();
                return timeRepToDelete;
            }
            return null;
        }
        public async Task<TimeReport> Update(TimeReport Entity)
        {
            var timeRepToUpdate = await _tidContext.TimeReports.FirstOrDefaultAsync
                (tr => tr.ID == Entity.ID);
            if (timeRepToUpdate != null)
            {
                timeRepToUpdate.CheckIn = Entity.CheckIn;
                timeRepToUpdate.CheckOut = Entity.CheckOut;
                timeRepToUpdate.CheckStatus = Entity.CheckStatus;

                await _tidContext.SaveChangesAsync();
                return timeRepToUpdate;
            }
            return null;
        }

        public async Task<IEnumerable<TimeReport>> GetWorkHours(int employeeId, DateTime startDate, DateTime endDate)
        {
            var reportsToGet = (from tr in _tidContext.TimeReports
                                where tr.EmployeeId == employeeId && tr.CheckIn >= startDate && tr.CheckOut <= endDate
                                select tr).ToListAsync();
            return await reportsToGet;  
        }

        //public TimeReport CheckIn(TimeReport newTime, int empId)
        //{
        //     throw new NotImplementedException();
        //var timeRepToAdd = (from time in _tidContext.TimeReports
        //                          select new TimeReport
        //                          {
        //                              CheckIn = DateTime.Now,
        //                              CheckStatus = true,
        //                              EmployeeId = empId
        //                          }).ToList();

        //_tidContext.AddAsync(timeRepToAdd);
        //_tidContext.SaveChangesAsync();
        //return timeRepToAdd;
        //}
    }
}
