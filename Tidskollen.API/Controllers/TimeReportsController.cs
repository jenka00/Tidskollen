using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tidskollen.API.Services;
using Tidskollen.Models;

namespace Tidskollen.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeReportsController : ControllerBase
    {
        private ITidskollen<TimeReport> _tidskollen;

        public TimeReportsController(ITidskollen<TimeReport> tidskollen)
        {
            _tidskollen = tidskollen;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTimeReports()
        {
            try
            {
                return Ok(await _tidskollen.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TimeReport>> GetTimeReport(int id)
        {
            try
            {
                var trToGet = await _tidskollen.GetSingle(id);
                if (trToGet == null)
                {
                    return NotFound();
                }
                return trToGet;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        [HttpPost]
        public async Task<ActionResult<TimeReport>> AddTimeReport(TimeReport newTimeReport)
        {
            try
            {
                if (newTimeReport == null)
                {
                    return BadRequest();
                }
                var trToCreate = await _tidskollen.Add(newTimeReport);
                return CreatedAtAction(nameof(GetTimeReport), new { id = trToCreate.ID }, trToCreate);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att skapa ny tidsrapport.");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TimeReport>> UpdateTimeReport(int id, TimeReport updateTR)
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
                return await _tidskollen.Update(updateTR);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att hämta tidsrapport från databas.");
            }
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<TimeReport>> DeleteTimeReport(int id)
        {
            try
            {
                var trToDelete = await _tidskollen.GetSingle(id);
                if (trToDelete == null)
                {
                    return NotFound($"Misslyckat försök att hitta tidsrapport med ID-nummer {id}.");
                }
                return await _tidskollen.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Misslyckat försök att ta bort tidsrapport med id {id}.");
            }
        }
    }
}
