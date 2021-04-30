using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qs.Messages.ApplicationServices.Models;
using qs.Messages.ApplicationServices.Services.Interfaces;

namespace qs.Messages.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController: ApiBaseController
    {
        readonly ITemplateService _templateService;
        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TemplateModel model)
        {
            await _templateService.Create(model);
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update([FromBody] TemplateModel model)
        {
            _templateService.Update(model);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var model = await _templateService.GetByID(id);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _templateService.Delete(id);
            return NoContent();
        }

        [HttpGet()]
        public IActionResult List(Guid? projecID, string id, string description)
        {
            var model = _templateService.Search(projecID, id, description);
            return Ok(model);
        }
    }
}