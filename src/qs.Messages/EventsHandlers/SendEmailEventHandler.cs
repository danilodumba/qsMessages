using System.Threading.Tasks;
using MediatR;
using qs.Messages.ApplicationServices.Command;
using qs.Messages.Pack.Events;
using Rebus.Handlers;

namespace qs.Messages.Pack.EventsHandlers
{
    public class SendEmailEventHandler :
        IHandleMessages<SendMailEvent>
    {
        readonly IMediator _mediator;
        public SendEmailEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(SendMailEvent message)
        {
            var command = new SendMailCommand
            {
                To = message.To,
                TemplateID = message.TemplateID,
                ProjectApiKey = message.ProjectApiKey
            };

            await _mediator.Send(command);
        }
    }
}