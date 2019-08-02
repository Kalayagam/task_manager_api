using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Business.Interfaces;
using TaskManager.Core;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private readonly ITaskBusiness _taskManagerBusiness;
        public TaskManagerController(ITaskBusiness taskManagerBusiness)
        {
            _taskManagerBusiness = taskManagerBusiness;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskManagerBusiness.GetAll();
            return Ok(tasks);
        }

        [HttpGet("parent")]
        public async Task<IActionResult> GetAllParentTasks()
        {
            var tasks = await _taskManagerBusiness.GetAllParent();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _taskManagerBusiness.Get(id);
            return Ok(task);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddTask([Required]TaskViewModel taskViewModel)
        {
            await _taskManagerBusiness.Add(taskViewModel);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateTask([Required]TaskViewModel taskViewModel)
        {
            await _taskManagerBusiness.Update(taskViewModel);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskManagerBusiness.Delete(id);
            return NoContent();
        }
    }
}