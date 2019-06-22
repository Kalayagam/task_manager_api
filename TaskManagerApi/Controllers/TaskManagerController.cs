using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business;
using TaskManager.Core;
using TaskManager.Core.Exceptions;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private readonly ITaskManagerBusiness _taskManagerBusiness;
        public TaskManagerController(ITaskManagerBusiness taskManagerBusiness)
        {
            _taskManagerBusiness = taskManagerBusiness;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _taskManagerBusiness.GetAllTasks();
                return Ok(tasks);                 
            }
            catch(TaskDetailsException ex)
            {
                return HandleException(ex);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var task = await _taskManagerBusiness.GetTask(id);
                return Ok(task);
            }
            catch (TaskDetailsException ex)
            {
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddTask(TaskViewModel taskViewModel)
        {           

            try
            {               
                await _taskManagerBusiness.AddTask(taskViewModel);
            }
            catch (TaskDetailsException ex)
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
        public async Task<IActionResult> UpdateTask(TaskViewModel taskViewModel)
        {
            try
            {
                await _taskManagerBusiness.UpdateTask(taskViewModel);
                return NoContent();
            }
            catch (TaskDetailsException ex)
            {
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }


        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                await _taskManagerBusiness.DeleteTask(id);
            }
            catch (TaskDetailsException ex)
            {
                return HandleException(ex);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);
            }

            return NoContent();
        }


        private IActionResult HandleException(TaskDetailsException ex)
        {
            switch (ex.ErrorNumber)
            {
                case ErrorCodes.TaskNotFoundResponse:
                    return NotFound(ex.Message);
                case ErrorCodes.TaskBadRequestResponse:
                    return BadRequest(ex.Message);
                case ErrorCodes.TaskInternalServerResponse:
                    return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                default:
                    return StatusCode((int)HttpStatusCode.BadGateway, ex.Message);

            }
        }

    }
}