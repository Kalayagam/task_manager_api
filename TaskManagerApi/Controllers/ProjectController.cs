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
        private readonly IBusiness<ProjectViewModel> _projectBusiness;
        public ProjectController(IBusiness<ProjectViewModel> projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projeccts = await _projectBusiness.GetAll();
            return Ok(projeccts);
        }
       
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _projectBusiness.Get(id);
            return Ok(project);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(ProjectViewModel projectViewModel)
        {
            await _projectBusiness.Add(projectViewModel);

            return StatusCode((int)HttpStatusCode.Created);
        }
        
        [HttpPut("")]
        public async Task<IActionResult> Put(ProjectViewModel projectViewModel)
        {
            await _projectBusiness.Update(projectViewModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectBusiness.Delete(id);

            return NoContent();
        }
    }
}
