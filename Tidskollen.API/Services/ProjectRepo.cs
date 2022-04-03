using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Model;
using Tidskollen.Models;

namespace Tidskollen.API.Services
{
    public class ProjectRepo : ITidskollen<Project>
    {
        private TidDbContext _tidContext;
        public ProjectRepo(TidDbContext tidContext)
        {
            _tidContext = tidContext;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _tidContext.Projects.Include(ep => ep.EmployeeProject).ToListAsync();
        }

        public async Task<Project> GetSingle(int id)
        {
            return await _tidContext.Projects.Include(ep => ep.EmployeeProject)
                .FirstOrDefaultAsync(p => p.ProjectId == id);
        }
        public async Task<Project> Add(Project newProject)
        {
            var result = await _tidContext.Projects.AddAsync(newProject);
            await _tidContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Project> Delete(int id)
        {
            var result = await _tidContext.Projects.Where(e => e.ProjectId == id).FirstOrDefaultAsync();
            if (result != null)
            {
                _tidContext.Projects.Remove(result);
                await _tidContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Project> Update(Project project)
        {
            var projectToUpdate = await _tidContext.Projects.FirstOrDefaultAsync
                (p => p.ProjectId == project.ProjectId);
            if (projectToUpdate != null)
            {
                projectToUpdate.ProjectName = project.ProjectName;
             
                await _tidContext.SaveChangesAsync();
                return projectToUpdate;
            }
            return null;
        }
    }
}
