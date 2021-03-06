using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Dtos;
using Tidskollen.API.Services;
using Tidskollen.Models;

namespace Tidskollen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private ITidskollen<Employee> _tidskollen;
        private IMapper _mapper;

        public EmployeesController(ITidskollen<Employee> tidskollen, IMapper mapper)
        {
            _tidskollen = tidskollen;
            _mapper = mapper;
        }

        //Using EmployeeReadDto to show a list of all employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAllEmployees()
        {
            try
            {
                var employeesToGet = await _tidskollen.GetAll();
                return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employeesToGet));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        //Using EmployeeReadDto to show a list of a single employee
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeReadDto>> GetEmployee(int id)
        {
            try
            {
                var empToGet = await _tidskollen.GetSingle(id);
                if (empToGet == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<EmployeeReadDto>(empToGet));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }

        [HttpPost] //Using EmployeeCreateDto to create new employee and EmployeeReadDto to print show new employee
        public async Task<ActionResult<EmployeeReadDto>> AddEmployee(EmployeeCreateDto newEmployeeCreateDto)
        {
            try
            {
                if (newEmployeeCreateDto == null)
                {
                    return BadRequest();
                }
                var empToCreate = _mapper.Map<Employee>(newEmployeeCreateDto);
                await _tidskollen.Add(empToCreate);
                var employeeReadDto = _mapper.Map<EmployeeReadDto>(empToCreate);
                return CreatedAtAction(nameof(GetEmployee), new { id = employeeReadDto.EmployeeId }, employeeReadDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att skapa ny medarbetare.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee updateEmp)
        {
            try
            {
                if(id != updateEmp.EmployeeId)
                {
                    return BadRequest($"ID matchar inte.");
                }
                var empToUpdate = await _tidskollen.GetSingle(id);

                if(empToUpdate == null)
                {
                    return NotFound($"Medarbetare med ID-nummer {id} saknas.");
                }
                return await _tidskollen.Update(updateEmp);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att hämta medarbetare från databas.");
            }
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var empToDelete = await _tidskollen.GetSingle(id);
                if(empToDelete == null)
                {
                    return NotFound($"Misslyckat försök att hitta medarbetare med ID-nummer {id}.");
                }
                return await _tidskollen.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Misslyckat försök att ta bort " +
                    "medarbetaren.");
            }            
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllEmployees()
        //{
        //    try
        //    {
        //        return Ok(await _tidskollen.GetAll());
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError, 
        //            "Oväntat fel har uppstått i hämtningen av data från databas.");
        //    }
        //}

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Employee>> GetEmployee(int id)
        //{
        //    try
        //    {
        //        var empToGet = await _tidskollen.GetSingle(id);
        //        if(empToGet == null)
        //        {
        //            return NotFound();
        //        }
        //        return empToGet;
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Oväntat fel har uppstått i hämtningen av data från databas.");
        //    }
        //}

        //[HttpPost]
        //public async Task<ActionResult<Employee>> AddEmployee(Employee newEmployee)
        //{
        //    try
        //    {
        //        if (newEmployee == null)
        //        {
        //            return BadRequest();
        //        }
        //        var empToCreate = await _tidskollen.Add(newEmployee);
        //        return CreatedAtAction(nameof(GetEmployee), new { id = empToCreate.EmployeeId }, empToCreate);
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Misslyckat försök att skapa ny medarbetare.");
        //    }
        //}
    }
}
