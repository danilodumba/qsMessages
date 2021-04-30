using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using qs.Messages.ApplicationServices.Command;
using qs.Messages.Domains.Repositories;
using qsLibPack.Domain.ValueObjects.Br;

namespace qs.Messages.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ApiBaseController
    {
        readonly IMediator _mediator;
        readonly IEmailRepository _emailRepository;
        public EmailController(IMediator mediator, IEmailRepository emailRepository)
        {
            _mediator = mediator;
            _emailRepository = emailRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SendMailCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> Get(DateTime startDate, DateTime endDate)
        {
            var period = new PeriodoVO(startDate, endDate);
            var emails = await _emailRepository.Serach(period, null, null);
            
            var model = emails.Select(e => new {
                e.Date,
                e.Project.Name,
                e.Subject,
                status = e.Status.ToString()
            }).ToList();

            return Ok(model);
        }
    }
}