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
    public class EmployeeProjectsController : ControllerBase
    {
        private ITidskollen<EmployeeProject> _tidskollen;
        private IMapper _mapper;

        public EmployeeProjectsController(ITidskollen<EmployeeProject> tidskollen, IMapper mapper)
        {
            _tidskollen = tidskollen;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeProjectReadDto>>> GetAllEmpPro()
        {
            try
            {
                var empProjToGet = await _tidskollen.GetAll();
                return Ok(_mapper.Map<IEnumerable<EmployeeProjectReadDto>>(empProjToGet));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeProjectReadDto>> GetEmpPro(int id)
        {
            try
            {
                var empProToGet = await _tidskollen.GetSingle(id);
                if (empProToGet == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<EmployeeProjectReadDto>(empProToGet));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeProjectReadDto>> AddEmpPro(EmployeeProject newEmpPro)
        {
            try
            {
                if (newEmpPro == null)
                {
                    return BadRequest();
                }
                await _tidskollen.Add(newEmpPro);
                var empProToCreate = _mapper.Map<EmployeeProjectReadDto>(newEmpPro); 
                return CreatedAtAction(nameof(GetEmpPro), new
                { id = empProToCreate.EmployeeProjectId }, empProToCreate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att koppla medarbetare till projekt.");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeProject>> UpdateProject(int id, EmployeeProject updateEmpPro)
        {
            try
            {
                if (id != updateEmpPro.EmployeeProjectId)
                {
                    return BadRequest($"ID matchar inte.");
                }
                var empProToUpdate = await _tidskollen.GetSingle(id);

                if (empProToUpdate == null)
                {
                    return NotFound($"ID-nummer {id} saknas.");
                }
                return await _tidskollen.Update(updateEmpPro);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att uppdatera data till databas.");
            }
        }
        public async Task<ActionResult<EmployeeProject>> DeleteEmpPro(int id)
        {
            try
            {
                var empProToDelete = await _tidskollen.GetSingle(id);
                if (empProToDelete == null)
                {
                    return NotFound($"Misslyckat försök att hitta kopplingen med ID-nummer {id}.");
                }
                return await _tidskollen.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att ta bort kopplingen ur databas.");
            }
        }
    }
}
