using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qs.Messages.ApplicationServices.Models;
using qs.Messages.ApplicationServices.Services.Interfaces;

namespace qs.Messages.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ApiBaseController
    {
        readonly IUserService _userSevice;
        public UserController(IUserService userService)
        {
            _userSevice = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel model)
        {
            await _userSevice.Create(model);
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserModel model)
        {
            _userSevice.Update(model);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _userSevice.GetByID(id);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userSevice.Delete(id);
            return NoContent();
        }

        [HttpGet()]
        public IActionResult List(string name)
        {
            var model = _userSevice.ListByName(name);
            return Ok(model);
        }
    }
}