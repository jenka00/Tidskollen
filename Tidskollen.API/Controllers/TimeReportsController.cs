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
    public class TimeReportsController : ControllerBase
    {
        private ITidskollen<TimeReport> _tidskollen;
        private ITimeReport _tid;
        private IMapper _mapper;

        public TimeReportsController(ITidskollen<TimeReport> tidskollen, ITimeReport tid, IMapper mapper)
        {
            _tidskollen = tidskollen;
            _tid = tid;
            _mapper = mapper;
        }
        //Using TimerReportsReadDto to show all timereports with limitations in employee information) 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeReportsReadDto>>> GetAllTimeReports()
        {
            try
            {
                var timeReportToGet = await _tidskollen.GetAll();
                return Ok(_mapper.Map<IEnumerable<TimeReportsReadDto>>(timeReportToGet));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        //Using TimerReportsReadDto to show single timereports with limitations in employee information) 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TimeReportsReadDto>> GetTimeReport(int id)
        {
            try
            {
                var trToGet = await _tidskollen.GetSingle(id);
                if (trToGet == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<TimeReportsReadDto>(trToGet));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        [HttpGet("{workperiod}")]
        public async Task<ActionResult<TimeReport>> WorkPeriod(int empId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _tid.GetWorkHours(empId, startDate, endDate);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Oväntat fel har uppstått i hämtningen av data från databas.");
            }           
        }
        //Get by employeeId, override the RoutePrefix attribute
        [Route("~/api/employees/{employeeId:int}/timereports")]
        [HttpGet]      
        public async Task<ActionResult<TimeReport>> GetByEmployee(int employeeId)
        {
            try
            {
                var result = await _tid.GetByEmpID(employeeId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }            
        }
        [HttpPost]
        public async Task<ActionResult<TimeReportsReadDto>> AddTimeReport(TimeReportsCreateDto newTimeReportsCreateDto)
        {
            try
            {
                if (newTimeReportsCreateDto == null)
                {
                    return BadRequest();
                }
                var trToCreate = _mapper.Map<TimeReport>(newTimeReportsCreateDto);
                await _tidskollen.Add(trToCreate);
                var timeReportReadDto = _mapper.Map<TimeReportsReadDto>(trToCreate);
                return CreatedAtAction(nameof(GetTimeReport), new { id = timeReportReadDto.ID }, timeReportReadDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att skapa ny tidsrapport.");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TimeReportsReadDto>> UpdateTimeReport(int id, TimeReport updateTR)
        {
            try
            {
                if (id != updateTR.ID)
                {
                    return BadRequest($"ID matchar inte.");
                }
                var trToUpdate = await _tidskollen.GetSingle(id);

                if (trToUpdate == null)
                {
                    return NotFound($"Tidsrapporten med ID-nummer {id} saknas.");
                }
                return Ok(_mapper.Map<TimeReportsReadDto>(updateTR));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att hämta tidsrapport från databas.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TimeReportsReadDto>> DeleteTimeReport(int id)
        {
            try
            {
                var trToDelete = await _tidskollen.GetSingle(id);
                if (trToDelete == null)
                {
                    return NotFound($"Misslyckat försök att hitta tidsrapport med ID-nummer {id}.");
                }
                await _tidskollen.Delete(id);
                return Ok(_mapper.Map<TimeReportsReadDto>(trToDelete));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Misslyckat försök att ta bort tidsrapport med id {id}.");
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAllTimeReports()
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
    }
}
