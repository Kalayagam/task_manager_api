using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Interfaces;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Model;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectBusiness _projectBusiness;
        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var projeccts = await _projectBusiness.GetAll();
                return Ok(projeccts);
            }
            catch (ProjectException ex)
            {
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
        }

        // GET: api/Project/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var project = await _projectBusiness.Get(id);
                return Ok(project);
            }
            catch (ProjectException ex)
            {
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> Post(ProjectViewModel projectViewModel)
        {
            try
            {
                await _projectBusiness.Add(projectViewModel);
            }
            catch (ProjectException ex)
            {
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }


            return StatusCode((int)HttpStatusCode.Created);
        }
        
        [HttpPut("")]
        public async Task<IActionResult> Put(ProjectViewModel projectViewModel)
        {
            try
            {
                await _projectBusiness.Update(projectViewModel);
                return NoContent();
            }
            catch (ProjectException ex)
            {
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _projectBusiness.Delete(id);
            }
            catch (ProjectException ex)
            {
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }

            return NoContent();
        }


        private IActionResult HandleException(ProjectException ex)
        {
            switch (ex.ErrorNumber)
            {
                case ErrorCodes.ProjectNotFoundResponse:
                    return NotFound(ex.Message);
                case ErrorCodes.ProjectBadRequestResponse:
                    return BadRequest(ex.Message);
                case ErrorCodes.ProjectInternalServerResponse:
                    return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                default:
                    return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);

            }
        }
    }
}
