using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Interfaces;
using TaskManager.Core.Model;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBusiness<UserViewModel> _userBusiness;
        public UserController(IBusiness<UserViewModel> userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userBusiness.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userBusiness.Get(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([Required]UserViewModel userViewModel)
        {
            await _userBusiness.Add(userViewModel);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("")]
        public async Task<IActionResult> Put([Required]UserViewModel userViewModel)
        {
            await _userBusiness.Update(userViewModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userBusiness.Delete(id);
            return NoContent();
        }
    }
}