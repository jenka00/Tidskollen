using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Model;
using Tidskollen.API.Services;
using Tidskollen.Models;

namespace Tidskollen.API
{
    public class EmployeeRepo : ITidskollen<Employee>
    {
        private TidDbContext _tidContext;
        public EmployeeRepo(TidDbContext tidContext)
        {
            _tidContext = tidContext;
        }
        public async Task<Employee> Add(Employee newEmployee)
        {
            var empToAdd = await _tidContext.Employees.AddAsync(newEmployee);
            await _tidContext.SaveChangesAsync();
            return empToAdd.Entity;
        }

        public async Task<Employee> Delete(int id)
        {
            var empToDelete = await _tidContext.Employees.Where(e => e.EmployeeId == id).FirstOrDefaultAsync();
            if(empToDelete != null)
            {
                _tidContext.Employees.Remove(empToDelete);
                await _tidContext.SaveChangesAsync();
                return empToDelete;
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _tidContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetSingle(int id)
        {
            return await _tidContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee> Update(Employee employee)
        {
            var empToUpdate = await _tidContext.Employees.FirstOrDefaultAsync
                (e => e.EmployeeId == employee.EmployeeId);
            if (empToUpdate != null)
            {
                empToUpdate.FirstName = employee.FirstName;
                empToUpdate.LastName = employee.LastName;
                empToUpdate.DateOfBirth = employee.DateOfBirth;

                await _tidContext.SaveChangesAsync();
                return empToUpdate;
            }
            return null;
        }
    }
}
