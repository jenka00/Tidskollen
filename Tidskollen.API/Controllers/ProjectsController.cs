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
    public class ProjectsController : ControllerBase
    {
        private ITidskollen<Project> _tidskollen;
        private IMapper _mapper;

        public ProjectsController(ITidskollen<Project> tidskollen, IMapper mapper)
        {
            _tidskollen = tidskollen;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetAllProjects()
        {
            try
            {
                var projectsToGet = await _tidskollen.GetAll();
                return Ok(_mapper.Map<IEnumerable<ProjectReadDto>>(projectsToGet));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProjectReadDto>> GetProject(int id)
        {
            try
            {
                var projectToGet = await _tidskollen.GetSingle(id);
                if (projectToGet == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ProjectReadDto>(projectToGet));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        [HttpPost]
        public async Task<ActionResult<ProjectReadDto>> AddProject(ProjectCreateDto newProjectCreateDto)
        {
            try
            {
                if (newProjectCreateDto == null)
                {
                    return BadRequest();
                }
                var projectToCreate = _mapper.Map<Project>(newProjectCreateDto);
                await _tidskollen.Add(projectToCreate);
                var projectReadDto = _mapper.Map<ProjectReadDto>(projectToCreate);
                return CreatedAtAction(nameof(GetProject), new { id = projectReadDto.ProjectId }, projectReadDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att skapa nytt projekt.");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Project>> UpdateProject(int id, Project updateProject)
        {
            try
            {
                if (id != updateProject.ProjectId)
                {
                    return BadRequest($"ID matchar inte.");
                }
                var projectToUpdate = await _tidskollen.GetSingle(id);

                if (projectToUpdate == null)
                {
                    return NotFound($"Projekt med ID-nummer {id} saknas.");
                }
                return await _tidskollen.Update(updateProject);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Misslyckat försök att uppdatera projekt från databas.");
            }
        }
        [HttpDelete("{id}")]

        public async Task<ActionResult<Project>> DeleteProject(int id)
        {
            try
            {
                var projectToDelete = await _tidskollen.GetSingle(id);
                if (projectToDelete == null)
                {
                    return NotFound($"Misslyckat försök att hitta projektet med ID-nummer {id}.");
                }
                return await _tidskollen.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Misslyckat försök att ta bort projektet.");
            }
        }
    }
}