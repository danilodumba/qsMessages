using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qs.Messages.ApplicationServices.Models;
using qs.Messages.ApplicationServices.Services.Interfaces;

namespace qs.Messages.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController: ApiBaseController
    {
        readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectModel model)
        {
            var id = await _projectService.Create(model);
            if (id != Guid.Empty)
                return Ok(id);

            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProjectModel model)
        {
            _projectService.Update(model);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = await _projectService.GetByID(id);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _projectService.Delete(id);
            return NoContent();
        }

        [HttpGet()]
        public IActionResult List(string name)
        {
            var model = _projectService.ListByName(name);
            return Ok(model);
        }
    }
}