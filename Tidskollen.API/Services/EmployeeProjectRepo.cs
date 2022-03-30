using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Model;
using Tidskollen.Models;

namespace Tidskollen.API.Services
{
    public class EmployeeProjectRepo : ITidskollen<EmployeeProject>
    {
        private TidDbContext _tidContext;
        public EmployeeProjectRepo(TidDbContext tidContext)
        {
            _tidContext = tidContext;
        }

        public async Task<EmployeeProject> Add(EmployeeProject newEmpPro)
        {
            var result = await _tidContext.EmployeeProjects.AddAsync(newEmpPro);
            await _tidContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<EmployeeProject> Delete(int id)
        {
            var result = await _tidContext.EmployeeProjects.
                Where(e => e.EmployeeProjectId == id).FirstOrDefaultAsync();
            if (result != null)
            {
                _tidContext.EmployeeProjects.Remove(result);
                await _tidContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<EmployeeProject>> GetAll()
        {
            return await _tidContext.EmployeeProjects.ToListAsync();
        }

        public async Task<EmployeeProject> GetSingle(int id)
        {
            return await _tidContext.EmployeeProjects.FirstOrDefaultAsync(p => p.EmployeeProjectId == id);
        }

        public async Task<EmployeeProject> Update(EmployeeProject Entity)
        {
            var empProToUpdate = await _tidContext.EmployeeProjects.FirstOrDefaultAsync
                (p => p.EmployeeProjectId == Entity.EmployeeProjectId);
            if (empProToUpdate != null)
            {
                empProToUpdate.ProjectId = Entity.ProjectId;
                empProToUpdate.EmployeeId = Entity.EmployeeId;
                empProToUpdate.StartDate = Entity.StartDate;
                empProToUpdate.EndDate = Entity.EndDate;

                await _tidContext.SaveChangesAsync();
                return empProToUpdate;
            }
            return null;
        }
    }
}
