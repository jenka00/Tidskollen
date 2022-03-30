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
    public class ProjectsController : ControllerBase
    {
        private ITidskollen<Project> _tidskollen;

        public ProjectsController(ITidskollen<Project> tidskollen)
        {
            _tidskollen = tidskollen;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
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
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            try
            {
                var projectToGet = await _tidskollen.GetSingle(id);
                if (projectToGet == null)
                {
                    return NotFound();
                }
                return projectToGet;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Oväntat fel har uppstått i hämtningen av data från databas.");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Project>> AddProject(Project newProject)
        {
            try
            {
                if (newProject == null)
                {
                    return BadRequest();
                }
                var projectToCreate = await _tidskollen.Add(newProject);
                return CreatedAtAction(nameof(GetProject), new 
                { id = projectToCreate.ProjectId }, projectToCreate);
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