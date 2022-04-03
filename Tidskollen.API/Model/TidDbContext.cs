using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.Models;

namespace Tidskollen.API.Model
{
    public class TidDbContext : DbContext
    {
        public TidDbContext(DbContextOptions<TidDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeReport> TimeReports { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 1,
                FirstName = "Therese",
                LastName = "Brorsson",
                DateOfBirth = new DateTime(1990,03,25)
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 2,
                FirstName = "Julia",
                LastName = "Karlsson",
                DateOfBirth = new DateTime(1987, 02, 28)
            });
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = 3,
                FirstName = "Louisa",
                LastName = "Stark",
                DateOfBirth = new DateTime(1985, 12, 09)
            });
            modelBuilder.Entity<Project>().HasData(new Project
            {
                ProjectId = 1,
                ProjectName = "TomatOdling"
            });
            modelBuilder.Entity<Project>().HasData(new Project
            {
                ProjectId = 2,
                ProjectName = "Tulpaner"
            });
            //modelBuilder.Entity<TimeReport>().HasData(new TimeReport
            //{
            //    ID = 1,
            //    EmployeeId = 1,
            //    CheckIn = DateTime.Parse("2022-01-07,08:05:00"),
            //    CheckOut = DateTime.Parse("2022-01-07,17:02:00"),
            //    CheckStatus = false
            //});
            //modelBuilder.Entity<TimeReport>().HasData(new TimeReport
            //{
            //    ID = 2,
            //    EmployeeId = 4,
            //    CheckIn = new DateTime(2022,01,07,08,00,00),
            //    CheckOut = new DateTime(2022, 01, 07, 16, 10, 00),
            //    CheckStatus = false
            //});
        }
    }
}
